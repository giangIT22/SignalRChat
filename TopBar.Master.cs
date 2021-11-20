﻿using SignalRChat.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRChat
{
    public partial class TopBar : System.Web.UI.MasterPage
    {
        public bool IsChangingPassword = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var user = (User)Session[AppConst.SessionCurrentUserKey];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                if (user.Photo.Length == 0)
                {
                    changingAvatar.ImageUrl = @".\Models\default_data\default-avatar.png";
                    currentUserAvatar.ImageUrl = @".\Models\default_data\default-avatar.png";
                }
                else
                {
                    String strFileBase64 = Convert.ToBase64String(user.Photo);
                    changingAvatar.ImageUrl = "data:image/png;base64," + strFileBase64;
                    changingAvatar.DataBind();
                    currentUserAvatar.ImageUrl = "data:image/png;base64," + strFileBase64;
                    currentUserAvatar.DataBind();
                }
            }

        }

        protected void saveChangeAvatar_Click(object sender, EventArgs e)
        {
            var user = (User)Session[AppConst.SessionCurrentUserKey];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!fAvatar.HasFile) return;
            bool IsSuccess = false;
            string msg = "";
            (IsSuccess,msg) = UserFunc.UpdateAvatar(user.Id, fAvatar.FileBytes);
            if (IsSuccess)
            {
                user.Photo = fAvatar.FileBytes;
                String strFileBase64 = Convert.ToBase64String(user.Photo);
                Session[AppConst.SessionCurrentUserKey] = user;
                changingAvatar.ImageUrl = "data:image/png;base64," + strFileBase64;
                currentUserAvatar.ImageUrl = "data:image/png;base64," + strFileBase64;
            }
            // Reload image on nav bar
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            var user = (User)Session[AppConst.SessionCurrentUserKey];
            if (user == null)
            {
                Response.Redirect("Login.aspx");
            }

            if(txtNewPassword.Text != txtReNewPassword.Text)
            {
                lbErrorMessage.Text = "Error: Nhập lại mật khẩu mới không khớp!";
                IsChangingPassword = true;
                return;
            }

            string msg = "";
            bool IsSuccess = false;
            (IsSuccess, msg) = UserFunc.UpdatePassword(user.Username,txtCurrentPassword.Text, txtNewPassword.Text);

            if (!IsSuccess)
            {
                IsChangingPassword = true;
                lbErrorMessage.Text = "Error: " + msg;
                return;
            }

            IsChangingPassword = false;
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtReNewPassword.Text = "";
            lbErrorMessage.Text = "";

        }
    }
}