CREATE TABLE [dbo].[Article] (
    [id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [imageSource] NVARCHAR (100) NULL,
    [sellerId]    BIGINT         NOT NULL,
    [state]       NVARCHAR (20)  NOT NULL,
    [name]        NVARCHAR (50)  NOT NULL,
    [price]       FLOAT (53)     NOT NULL,
    [description] NVARCHAR (200) NULL,
    [category]    NVARCHAR (30)  NULL,
    [sex]         NVARCHAR (10)  NULL,
    [brand]       NVARCHAR (20)  NULL,
    [condition]   INT            NOT NULL,
    [createdAt]   DATETIME       NOT NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Article_User] FOREIGN KEY ([sellerId]) REFERENCES [dbo].[User] ([id]) ON DELETE CASCADE
);





