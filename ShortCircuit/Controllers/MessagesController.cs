using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ShortCircuit
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public List<string> randomResponses = new List<string>();
        public Dictionary<string, string> scriptedResponses = new Dictionary<string, string>();

        public MessagesController()
        {
            SetupScripts();
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                string response = GetResponse(message.Text);

                // return our reply to the user
                return message.CreateReplyMessage($"{response}");
            }
            else
            {
                return HandleSystemMessage(message);
            }
        }

        private string GetResponse(string text)
        {
            List<string> words = text.Split(' ').ToList();

            foreach(var word in words)
            {
                string response = string.Empty;
                scriptedResponses.TryGetValue(word, out response);
                if(!string.IsNullOrEmpty(response))
                {
                    return response;
                }
            }

            return GetRandomResponse();
        }

        private string GetRandomResponse()
        {
            Random rand = new Random();
            return randomResponses[rand.Next(0, randomResponses.Count - 1)];
        }

        private void SetupScripts()
        {
            randomResponses.Add("Malfunction. Need input.");
            randomResponses.Add("Hey, laser lips, your mama was a snow blower!");
            randomResponses.Add("Number 5 is alive.");
            randomResponses.Add("Wouldn't you like to be a Pepper too?");
            randomResponses.Add("Number 5 stupid name... want to be Kevin or Dave!");
            randomResponses.Add("Bird. Raven. Nevermore.");
            randomResponses.Add("Hmmmm. Oh, I get it! Ho ho ho ho ho ho ho ho ho ho ho! Hee hee hee hee hee hee hee hee hee! Nyuk, nyuk nyuk nyuk nyuk nyuk nyuk nyuk nyuk!");
            randomResponses.Add("Not malfunction. Number 5 is alive.)");
            randomResponses.Add("Many fragments. Some large, some small.");
            randomResponses.Add("Fish. Salmon. Sushi.");
            randomResponses.Add("No disassemble Number Five!");
            randomResponses.Add("Well, if you gotta go, don't squeeze the Charmin.");
            randomResponses.Add("Frankie, you broke the unwritten law. You ratted on your friends. When you do that Frankie, your enemies don't respect you. You got no friends no more. You got nobody, Frankie.");
            randomResponses.Add("Error. Grasshopper disassembled... Re-assemble!");
            randomResponses.Add("Ah don't worry little lady, I'll fix their wagon.");
            randomResponses.Add("Well, I guess that waps you up, you wascally wobot - huhgh-huhgh-huhgh-huhgh-huhgh-huhgh-huhgh-huhgh-huhgh!");
            randomResponses.Add("No disassemble!");

            scriptedResponses.Add("shit", "No shit. Where see shit?");
            scriptedResponses.Add("dog", "Beautiful animal... canine... dog... mutt.");
            scriptedResponses.Add("kill", "Disassemble?");
            scriptedResponses.Add("ass", "Kick ass? Donkey, mule, burrow");
            scriptedResponses.Add("jerk", "Jerk of the world: Turkey, idiot, pain in the ass.");
        }

        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }

            return null;
        }
    }
}