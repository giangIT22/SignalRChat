﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TopBar.Master" AutoEventWireup="true" CodeBehind="ChatBox.aspx.cs" Inherits="SignalRChat.Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SignalR Chat : Login</title>

    <script src="signalr/hubs"></script>
    <link rel="stylesheet" href="Styles/ChatBox.css">

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/style.css" rel="stylesheet" />
    <link href="Content/font-awesome.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <input id="current-receiver" type="text" value=""/>
        
        <div id="chat-container">

            <div id="chat-box">
                <div id="message-box">

                </div>
                <div id="function-bar">
                    <textarea id="ChatTextArea" cols="20" rows="1" disabled></textarea>

                    <label for="inputFile" id="inputFileLbl">
                        Upload
                         <input id='inputFile' type="file" value="" />
                    </label>

                    <button id="btnSend" type="button" disabled>Gửi</button>
                </div>
            </div>

            <div id="contact_list">
                
            </div>

        </div>
        
    
        <!--<button type="button" onclick="Test()" >click me</button>-->
    <script>
        $(function () {
            // Đổi in đậm phần navigation

            var nav_options = $('.nav-option').toArray();
            console.log("nav_opts: ", nav_options);
            nav_options.forEach(
                e => {
                    console.log("e.datapage: ", e.dataset.page);
                    if (e.dataset.page == 'ChatBox') {
                        e.classList = "nav-option active";

                    } else {
                        e.classList = "nav-option";
                    }
                    
                })
        });

    </script>
    <!-- scripp -->
    <script src="Scripts/ChatBox/ChatBox.js"></script>
    <script>
        // Global JS variable
        let current_user_id = '<%=this.currentUser.Id%>';
        let current_user_name = '<%=this.currentUser.Name%>';
        let DEFAULT_FILE_VALUE = {
            name: '',
            content: '',
            type: '',
        };
        let gbl_file_variable = DEFAULT_FILE_VALUE;
    </script>
 
</asp:Content>

