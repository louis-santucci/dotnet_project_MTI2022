CREATE TABLE [dbo].[Cart] (
    [id]        BIGINT NOT NULL,
    [buyerId]   BIGINT NOT NULL,
    [articleId] BIGINT NOT NULL,
    [quantity]  INT    NOT NULL,
    CONSTRAINT [PK_Cart_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Cart_Article] FOREIGN KEY ([articleId]) REFERENCES [dbo].[Article] ([id]),
    CONSTRAINT [FK_Cart_User] FOREIGN KEY ([buyerId]) REFERENCES [dbo].[User] ([id])
);

