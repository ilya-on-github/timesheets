using System;

namespace Timesheets.Services.Commands.Issues
{
    public class Issue
    {
        public Guid Id { get; }
        public string Summary { get; private set; }
        public string Description { get; private set; }
        public Guid AccountId { get; private set; }

        public Issue(string summary, string description, AccountDescriptor account)
        {
            Id = Guid.NewGuid();
            SetSummary(summary);
            SetDescription(description);
            SetAccount(account);
        }

        public Issue(Guid id, string summary, string description, Guid accountId)
        {
            Id = id;
            Summary = summary;
            Description = description;
            AccountId = accountId;
        }

        public IssueDescriptor Descriptor()
        {
            return new IssueDescriptor(Id, Summary);
        }

        public void Update(string summary, string description, AccountDescriptor account)
        {
            SetSummary(summary);
            SetDescription(description);
            SetAccount(account);
        }

        private void SetDescription(string description)
        {
            Description = description?.Trim().RemoveDoubleSpaces();
        }

        private void SetSummary(string summary)
        {
            if (string.IsNullOrWhiteSpace(summary))
            {
                throw new ArgumentException("Issue summary can't be empty.");
            }

            Summary = summary.Trim().RemoveDoubleSpaces();
        }

        private void SetAccount(AccountDescriptor account)
        {
            if (account == null)
            {
                throw new ArgumentException("Account have to be provided.");
            }

            AccountId = account.Id;
        }
    }
}