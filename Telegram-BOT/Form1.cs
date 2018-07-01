using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using System.Net;
using System.IO;

namespace Telegram_BOT
{ 
    public partial class Form1 : Form
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("486689643:AAEa-alCf2z2MC8JUygZ_VU12tQq8iOENFQ");

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Add vertical scroll bars to the TextBox control.
            textBox1.ScrollBars = ScrollBars.Vertical;
            // Allow the TAB key to be entered in the TextBox control.
            textBox1.AcceptsReturn = true;
            // Allow the TAB key to be entered in the TextBox control.
            textBox1.AcceptsTab = true;
            // Set WordWrap to true to allow text to wrap to the next line.
            textBox1.WordWrap = true;

            button3.Enabled = false;
            button5.Enabled = false;
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnMessageEdited += Bot_OnEdited;
            Bot.OnReceiveError += Bot_OnError;
        }

        private void Bot_OnError(object sender, ReceiveErrorEventArgs e)
        {
            MessageBox.Show("Ошибка!");
        }

        private void Bot_OnEdited(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                Bot.SendTextMessageAsync(e.Message.Chat.Id, "Дурак штоле? Зачем меняешь сообщение?!", replyToMessageId: e.Message.MessageId);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Bot.IsReceiving == true)
                Bot.StopReceiving();

            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button5.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            textBox1.Text += "Бот начал работу!" + Environment.NewLine + Environment.NewLine;
            Bot.StartReceiving();
        }

        private void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                string Message_ = e.Message.Text;
                long id = e.Message.Chat.Id;
                string name = e.Message.Chat.FirstName;

                ReplyKeyboardMarkup ReplyKeyboard = new[] {

                        new[] { "Получить фото случайного котика 🐱", "Получить фото случайного Сибиряка 🐵" },
                        new[] { "Получить фото Жени 🐷", "Получить фото Яны 🦆" },
                        new[] { "Получить фото крутых мужиков 😎" },
                };

                Bot.SendTextMessageAsync(e.Message.Chat.Id, "Минуточку...", replyMarkup:ReplyKeyboard);

                if (Message_ == "/start")
                    Bot.SendTextMessageAsync(id, "Приветствую тебя, " + name + "\nДля информации нажми /help");

                else if (Message_ == "/help")
                    Bot.SendTextMessageAsync(id, " Команды: \n/cat - отправить случайного котика\n/yana - отправить Яну\n/jeca - отправить Жеку\n/sibiryaka - получить фото случайного Сибиряка.\n/krutoi_mugik - получить фото случайного крутого мужика");

                else if (Message_ == "/yana" || Message_ == "Получить фото Яны 🦆")
                {
                    //Bot.SendTextMessageAsync(id, "Отправляю...");
                    Bot.SendTextMessageAsync(id, "https://bit.ly/2yWjOyT");
                }

                else if (Message_ == "/jeca" || Message_ == "Получить фото Жени 🐷")
                {
                    //Bot.SendTextMessageAsync(id, "Отправляю...");
                    Bot.SendTextMessageAsync(id, "https://bit.ly/2lNVy8J");
                }

                else if (Message_ == "/sibiryaka" || Message_ == "Получить фото случайного Сибиряка 🐵")
                {
                    string[] arr2 = new string[9] {

                        "https://ibb.co/kwKj4y",
                        "https://ibb.co/nN9j4y",
                        "https://ibb.co/hVUj4y",
                        "https://ibb.co/ingxPy",
                        "https://ibb.co/dmXhrd",
                        "https://ibb.co/ep2hrd",
                        "https://ibb.co/ehAfcJ",
                        "https://ibb.co/fFWWjy",
                        "https://ibb.co/kHDNrd"
                    };

                    Random rand = new Random();
                   // Bot.SendTextMessageAsync(id, "Отправляю...");
                    Bot.SendTextMessageAsync(id, arr2[rand.Next(0, 9)]);
                }

                else if (Message_ == "/krutoi_mugik" || Message_ == "Получить фото крутых мужиков 😎")
                {
                    string[] arr3 = new string[2] {

                        "https://ibb.co/k0CbHJ",
                        "https://bit.ly/2KD3MLJ"
                    };

                    Random rand = new Random();
                    //Bot.SendTextMessageAsync(id, "Отправляю...");
                    Bot.SendTextMessageAsync(id, arr3[rand.Next(0, 2)]);
                }

                else if (Message_ == "Как дела?")
                    Bot.SendTextMessageAsync(id, "Нормас, а у тебя?");

                else if (Message_ == "Привет" || Message_ == "Привки")
                    Bot.SendTextMessageAsync(id, "Здарова, " + e.Message.Chat.FirstName);

                else if (Message_ == "/cat" || Message_ == "Получить фото случайного котика 🐱")
                {
                    Random rand = new Random();
                    int tmp = rand.Next(0, 1670);

                    string url = "http://random.cat/view/" + tmp;
                    string str = getResponse(@url).Replace("<img src=\"", "~").Replace("\" alt=\"\" title=\"\" id=\"cat\" /></a>", "~");
                    string[] split = str.Split('~');

                    Bot.SendTextMessageAsync(id, split[1]);
                }

                else if (Message_.IndexOf("Спасибо") > -1 || Message_.IndexOf("cпасибо") > -1)
                    Bot.SendTextMessageAsync(id, "Не за что!");

                else
                    Bot.SendTextMessageAsync(id, "Даже не знаю как на это ответить...");

                textBox1.Text += "[ "+ e.Message.Date.TimeOfDay + 3 + " ]  Сообщение от " + id + " ( " + name + " ): " + Message_ + Environment.NewLine;
            }
            else
            {
                Bot.SendTextMessageAsync(e.Message.Chat.Id, "Дебик, не отправляй мне стикеры, я их все равно не пойму!", replyToMessageId: e.Message.MessageId);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = false;
            button5.Enabled = false;
            textBox1.Text += "Бот закончил работу!" + Environment.NewLine + Environment.NewLine;
            Bot.StopReceiving();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // сообщение
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bot.SendTextMessageAsync(comboBox1.Text, textBox3.Text);
            comboBox1.Items.Add(comboBox1.Text);

            textBox1.Text += "Отправлено сообщение пользователю: " + comboBox1.Text + Environment.NewLine;
        }

        // HTML parser
        string getResponse(string uri)
        {
            StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                    sb.Append(Encoding.Default.GetString(buf, 0, count));
            }

            while (count > 0);
            return sb.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
