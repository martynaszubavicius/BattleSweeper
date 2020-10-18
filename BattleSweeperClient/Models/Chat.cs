namespace BattleSweeperClient.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        public Chat(string author, string text)
        {
            Author = author;
            Text = text;
        }

        public Chat()
        {
        }

        public override string ToString()
        {
            return string.Format("{0}: '{1}' says: \"{2}\"", this.Id, this.Author, this.Text);
        }
    }
}
