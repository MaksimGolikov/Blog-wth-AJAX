﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Service;
using Blog.Models;
using Blog.Models.Chatt;
using Blog.ViewModels;

namespace Blog.Controllers
{
    public class ChatController : Controller
    {
        ChatService chatService;

        public ChatController()
        {
            chatService = new ChatService();
        }




        // GET: Chat
        public ActionResult Index()
        {
            var user = chatService.GetUser(User.Identity.Name);
            var createdChat = chatService.GetAllCreatedChat(user.Id) as List<Chat>;
            var myChat = chatService.GetAllUsedChats(user.Id) as List<Chat>;


            ChatViewModel model = new ChatViewModel() { MyChats = myChat,
                                                        ICreated = createdChat,
                                                        AllUsers = chatService.GetAllUsers(),
                                                        MasterPage= UpdateMasterPageData()
                                                       };           
            return View("Index",model);
        }
        


        private ForMasterPage UpdateMasterPageData()
        {
            var newMode = new ForMasterPage();
            var user = chatService.GetUser(User.Identity.Name);

            if (!User.Identity.IsAuthenticated || user == null)
            {
                newMode.UserAuthorisaed = false;
                newMode.UserName = "Guest";
                newMode.UserRole = "guest";
            }
            if (User.Identity.IsAuthenticated && user != null)
            {
                newMode.UserAuthorisaed = true;
                newMode.UserName = user.FirstName;
                newMode.UserRole = user.Role;
            }
            newMode.Language = Session["language"].ToString();

            return newMode;
        }


    }
}