namespace trainingQuerySide.Entities
{
    public class User
    {
        private User(string id, int sequence, string userName, string accountId, string role)
        {
            Id = id;
            Sequence = sequence;
            AccountId = accountId;
            UserName = userName;
            Role = role;
        }
        public string Id { get; set; }
        public int Sequence { get; private set; }
        public string UserName { get; private set; }
        public string AccountId { get; private set; }
        public string Role { get; set; }
        public ICollection<UserSubscription> UserSubscriptions { get; } = [];
    }
}
