﻿namespace EmailsSender.Abstractions
{
    public interface IEmailSender
    {
        Task SendAsync(string toEmail, string subject, string message, CancellationToken cancellationToken);
    }
}
