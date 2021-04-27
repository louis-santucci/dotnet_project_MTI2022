CREATE TABLE [dbo].[User] (
    [id]       BIGINT         NOT NULL,
    [userName] NVARCHAR (50)  NOT NULL,
    [email]    NVARCHAR (50)  NOT NULL,
    [password] NVARCHAR (40)  NOT NULL,
    [name]     NVARCHAR (50)  NOT NULL,
    [address]  NVARCHAR (100) NOT NULL,
    [gender]   NVARCHAR (10)  NOT NULL,
    [note]     FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [IX_User] UNIQUE NONCLUSTERED ([id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_User_1]
    ON [dbo].[User]([userName] ASC);

