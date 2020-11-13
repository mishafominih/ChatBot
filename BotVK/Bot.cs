using VkNet.Enums.SafetyEnums;
using ApiAiSDK;
using ApiAiSDK.Model;
using System.Data;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotVK
{
    public static class Bot
    {
        private static ulong GroupID = 200134545;
        private static string GroupKey = "d37c506cfb6be71a703bede18cbc1954ac09ea32ca6c73ae66017093763314bdeb036a49825483299e9a4";
        private static VkApi api = new VkApi();
        static ApiAi ApiAi;
        static Dictionary<long?, Game> dict = new Dictionary<long?, Game>();
        public static void Start()
        {
            AIConfiguration config = new AIConfiguration("AIKey", SupportedLanguage.Russian);
            ApiAi = new ApiAi(config);
            api.Authorize(new ApiAuthParams() { AccessToken = GroupKey });
            while (true)
            {
                var s = api.Groups.GetLongPollServer(GroupID);
                var poll = api.Groups.GetBotsLongPollHistory(
                new BotsLongPollHistoryParams() { Server = s.Server, Ts = s.Ts, Key = s.Key, Wait = 25 });
                if (poll?.Updates == null) continue;
                foreach (var a in poll.Updates)
                {
                    if (a.Type == GroupUpdateType.MessageNew)
                    {
                        ProcessingMessage(a.Message.Body.ToLower(), a.Message.UserId);
                    }
                }
            }
        }

        private static void ProcessingMessage(string userMessage, long? userID)
        {
            if (dict.ContainsKey(userID))
            {
                SendMessage(dict[userID].GetSiti(userMessage), userID);
            }
            else
            {
                dict.Add(userID, new Game());
                SendMessage(dict[userID].GetSiti(userMessage), userID);
            }
        }

        private static void SendMessage(string message, long? userID)
        {
            Random rnd = new Random();
            api.Messages.Send(new MessagesSendParams
            {
                RandomId = rnd.Next(),
                UserId = userID,
                Message = message
            });
        }
    }
}
