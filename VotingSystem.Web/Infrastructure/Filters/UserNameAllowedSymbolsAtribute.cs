namespace VotingSystem.Web.Infrastructure.Filters
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class UserNameAllowedSymbolsAttribute : ValidationAttribute
    {
        private const string AllowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_";

        public override bool IsValid(object value)
        {
            var username = value as string;

            return username != null && this.ValidateSymbolsInUsername(username);
        }

        private bool ValidateSymbolsInUsername(string username)
        {
            for (int i = 0; i < username.Length; i++)
            {
                if (!AllowedCharacters.Contains(username[i].ToString()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}