CREATE TABLE [dbo].[Rating] (
    [articleId] BIGINT         NOT NULL,
    [buyerId]   BIGINT         NOT NULL,
    [note]      INT            NOT NULL,
    [comment]   NVARCHAR (200) NULL,
    CONSTRAINT [PK_Rating_1] PRIMARY KEY CLUSTERED ([articleId] ASC),
    CONSTRAINT [FK_Rating_Article] FOREIGN KEY ([articleId]) REFERENCES [dbo].[Article] ([id]),
    CONSTRAINT [FK_Rating_User] FOREIGN KEY ([buyerId]) REFERENCES [dbo].[User] ([id])
);

