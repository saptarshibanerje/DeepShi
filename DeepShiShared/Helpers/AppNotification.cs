using DeepShiShared.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeepShiShared
{
    public static class AppNotification
    {
        public static void ShowMessage(Controller pController, AppMessage pAppMessage)
        {
            if (pAppMessage != null)
                ShowMessage(pController, null, pAppMessage.Text, pAppMessage.Type, pAppMessage.Action);
        }
        public static void ShowMessage(Controller pController, string pMessage, MessageType pMessageType, string pAction = null)
        {
            ShowMessage(pController, null, pMessage, pMessageType, pAction);
        }

        public static void ShowMessage(Controller pController, string pTitle, string pMessage, MessageType pMessageType, string pAction = null)
        {
            if (!Enum.IsDefined(typeof(MessageType), pMessageType))
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(pMessage))
                pMessage = pMessage.Replace("\n", string.Empty).Replace("\r", string.Empty);

            if (!string.IsNullOrWhiteSpace(pMessage))
            {
                string strMessageTitle = "Invalid !";
                bool IsShowMessage = true;
                bool IsConfirmMessage = false;
                switch (pMessageType)
                {
                    case MessageType.Success:
                        {
                            IsConfirmMessage = true;
                            strMessageTitle = "Success !";
                        }
                        break;
                    case MessageType.Info:
                        {
                            strMessageTitle = "Information !";

                        }
                        break;
                    case MessageType.Warning:
                        {
                            IsConfirmMessage = true;
                            strMessageTitle = "Warning ! ";
                        }
                        break;
                    case MessageType.Error:
                        {
                            IsConfirmMessage = true;
                            strMessageTitle = "Error ! ";
                        }
                        break;
                }
                pController.TempData["MessageType"] = pMessageType.ToString().ToLower();
                pController.TempData["MessageTitle"] = string.IsNullOrWhiteSpace(pTitle) ? strMessageTitle : pTitle;
                pController.TempData["MessageBody"] = pMessage;
                pController.TempData["IsShowMessage"] = IsShowMessage ? "Y" : "N";
                pController.TempData["IsConfirmMessage"] = IsConfirmMessage ? "Y" : "N";
            }
        }
    }
}
