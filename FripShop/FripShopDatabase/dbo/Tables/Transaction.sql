CREATE TABLE [dbo].[Transaction] (
    [id]               BIGINT        NOT NULL,
    [articleId]        BIGINT        NOT NULL,
    [buyerId]          BIGINT        NOT NULL,
    [transactionState] NVARCHAR (20) NOT NULL,
    [lastUpdateAt]     DATETIME      NOT NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Transaction_Article] FOREIGN KEY ([articleId]) REFERENCES [dbo].[Article] ([id]),
    CONSTRAINT [FK_Transaction_User] FOREIGN KEY ([buyerId]) REFERENCES [dbo].[User] ([id])
);

