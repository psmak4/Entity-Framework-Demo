CREATE TABLE [dbo].[Posts] (
    [PostId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId] INT            NOT NULL,
    [Title]  NVARCHAR (50)  NOT NULL,
    [Body]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([PostId] ASC),
    CONSTRAINT [FK_Posts_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);

