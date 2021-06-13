﻿namespace Hinode.Izumi.Data.Models.UserModels
{
    public class UserCard : EntityBase
    {
        public long UserId { get; set; }
        public long CardId { get; set; }
        public virtual User User { get; set; }
        public virtual Card Card { get; set; }
    }
}
