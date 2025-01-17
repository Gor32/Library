USE [Library]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthorBook](
	[AuthorId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [UQ_AuthorBook] UNIQUE NONCLUSTERED 
(
	[AuthorId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorId] [int] IDENTITY(1,1) NOT NULL,
	[FullNmae] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookBookGenres](
	[BookGenreId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [UQ_BookBookGenres] UNIQUE NONCLUSTERED 
(
	[BookGenreId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookGenres](
	[BookGenreId] [int] IDENTITY(1,1) NOT NULL,
	[BookGenreName] [varchar](200) NOT NULL,
	[MoreLikeBookGenreId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookGenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[BookName] [nvarchar](200) NOT NULL,
	[BookSize] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientBook](
	[ClientId] [int] NOT NULL,
	[BookId] [int] NOT NULL,
 CONSTRAINT [UQ_ClientBook] UNIQUE NONCLUSTERED 
(
	[ClientId] ASC,
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientCategores](
	[ClientCategoryId] [int] NOT NULL,
	[CategoryName] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ClientCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[ClientFullName] [nvarchar](200) NOT NULL,
	[ClientCategoryId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AuthorBook]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBook_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Authors] ([AuthorId])
GO
ALTER TABLE [dbo].[AuthorBook] CHECK CONSTRAINT [FK_AuthorBook_AuthorId]
GO
ALTER TABLE [dbo].[AuthorBook]  WITH CHECK ADD  CONSTRAINT [FK_AuthorBooks_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([BookId])
GO
ALTER TABLE [dbo].[AuthorBook] CHECK CONSTRAINT [FK_AuthorBooks_BookId]
GO
ALTER TABLE [dbo].[BookBookGenres]  WITH CHECK ADD  CONSTRAINT [FK_BookBookGenres_BookGenreId] FOREIGN KEY([BookGenreId])
REFERENCES [dbo].[BookGenres] ([BookGenreId])
GO
ALTER TABLE [dbo].[BookBookGenres] CHECK CONSTRAINT [FK_BookBookGenres_BookGenreId]
GO
ALTER TABLE [dbo].[BookBookGenres]  WITH CHECK ADD  CONSTRAINT [FK_BookBookGenres_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([BookId])
GO
ALTER TABLE [dbo].[BookBookGenres] CHECK CONSTRAINT [FK_BookBookGenres_BookId]
GO
ALTER TABLE [dbo].[BookGenres]  WITH CHECK ADD  CONSTRAINT [FK_BookGenres_MoreLikeBookGenreId] FOREIGN KEY([MoreLikeBookGenreId])
REFERENCES [dbo].[BookGenres] ([BookGenreId])
GO
ALTER TABLE [dbo].[BookGenres] CHECK CONSTRAINT [FK_BookGenres_MoreLikeBookGenreId]
GO
ALTER TABLE [dbo].[ClientBook]  WITH CHECK ADD  CONSTRAINT [FK_ClientBook_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([BookId])
GO
ALTER TABLE [dbo].[ClientBook] CHECK CONSTRAINT [FK_ClientBook_BookId]
GO
ALTER TABLE [dbo].[ClientBook]  WITH CHECK ADD  CONSTRAINT [FK_ClientBook_ClientId] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
GO
ALTER TABLE [dbo].[ClientBook] CHECK CONSTRAINT [FK_ClientBook_ClientId]
GO
ALTER TABLE [dbo].[Clients]  WITH CHECK ADD  CONSTRAINT [FK_Clients_ClientCategory] FOREIGN KEY([ClientCategoryId])
REFERENCES [dbo].[ClientCategores] ([ClientCategoryId])
GO
ALTER TABLE [dbo].[Clients] CHECK CONSTRAINT [FK_Clients_ClientCategory]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Add_AuthorBook]    
    @AuthorId int, @BookId int
AS    
    BEGIN    
        INSERT  INTO AuthorBook    
                (AuthorId, BookId
             )    
        VALUES  ( @AuthorId ,@BookId      
             );   
		   
    END;   
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Add_Authors]    
    @FullNmae NVARCHAR(200)
AS    
    BEGIN    
 DECLARE @Id as BIGINT  
        INSERT  INTO Authors   
                (FullNmae
             )    
        VALUES  ( @FullNmae
             );   
		SET @Id = SCOPE_IDENTITY();   
        SELECT  @Id AS AuthorId;    
    END;   
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Add_BookBookGenrese]    
    @BookGenreId int, @BookId int
AS    
    BEGIN    
        INSERT  INTO BookBookGenres    
                (BookGenreId, BookId
             )    
        VALUES  ( @BookGenreId ,@BookId      
             );   
		   
    END;   
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_Add_BookGenres]    
    @BookGenreName NVARCHAR(200), @MoreLikeBookGenreId Int
AS    
    BEGIN    
 DECLARE @Id as BIGINT  
        INSERT  INTO BookGenres   
                (BookGenreName,MoreLikeBookGenreId
             )    
        VALUES  ( @BookGenreName, @MoreLikeBookGenreId      
             );   
		SET @Id = SCOPE_IDENTITY();   
        SELECT  @Id AS BookGenreId;    
    END;   
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Add_Books]    
      @BookName nvarchar(200), @BookSize Int
AS    
    BEGIN    
 DECLARE @Id as BIGINT 
        INSERT  INTO Books    
                (BookName, BookSize
             )    
        VALUES  ( @BookName, @BookSize      
             );   
		SET @Id = SCOPE_IDENTITY();   
        SELECT  @Id AS BookId;    
		   
    END;   
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Add_Client]    
    @ClientFullName NVARCHAR(200), @ClientCategoryId Int
AS    
    BEGIN    
 DECLARE @Id as BIGINT  
        INSERT  INTO Clients   
                (ClientFullName,ClientCategoryId
             )    
        VALUES  ( @ClientFullName, @ClientCategoryId      
             );   
		SET @Id = SCOPE_IDENTITY();   
        SELECT  @Id AS ClientId;    
    END;   
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Add_ClientBook]    
    @ClientId int, @BookId int
AS    
    BEGIN    
        INSERT  INTO ClientBook    
                (ClientId, BookId
             )    
        VALUES  ( @ClientId ,@BookId      
             );   
		   
    END;   
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Add_ClientCategores]    
    @CategoryName NVARCHAR(200), @ClientCategoryId Int
AS    
    BEGIN    
 DECLARE @Id as BIGINT  
        INSERT  INTO ClientCategores   
                (CategoryName,ClientCategoryId
             )    
        VALUES  ( @CategoryName, @ClientCategoryId      
             );   
		SET @Id = @ClientCategoryId;   
        SELECT  @Id AS ClientCategoryId;    
    END;   
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_Update_AuthorBook] 
		@AuthorBook INT,   
		@BookId int   ,
		@NewAuthorBook INT,   
		@NewBookId int   
	AS    
		BEGIN    

		UPDATE BookBookGenres 
		SET BookGenreId = @NewAuthorBook ,
			BookId = @NewBookId
		WHERE BookId = @BookId and  BookGenreId=@NewAuthorBook
	           
		END; 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Update_Authors] 
		@Id INT,   
		@FullNmae NVARCHAR(200)
	AS    
		BEGIN    

		UPDATE Authors SET  FullNmae= @FullNmae
		WHERE AuthorId = @Id 
	           
		END;    
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_Update_BookBookGenres] 
		@BookGenreId INT,   
		@BookId int   ,
		@NewBookGenreId INT,   
		@NewBookId int   
	AS    
		BEGIN    

		UPDATE BookBookGenres 
		SET BookGenreId = @NewBookGenreId ,
			BookId = @NewBookId
		WHERE BookId = @BookId and  BookGenreId=@BookGenreId
	           
		END;    
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Update_BookGenres] 
		@Id INT,   
		@BookGenreName NVARCHAR(200),
		@MoreLikeBookGenreId Int
	AS    
		BEGIN    

		UPDATE BookGenres SET  BookGenreName= @BookGenreName
		WHERE BookGenreId = @Id 
	           
		END;    
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[SP_Update_Books]   
		@BookId int   ,
		@BookName nvarchar(200),
		@BookSize Int
	AS    
		BEGIN    

		UPDATE Books
		SET BookName = @BookName,
			BookSize = @BookSize
		WHERE BookId = @BookId 
	           
		END;   
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Update_Client] 
		@Id INT,   
		@ClientFullName NVARCHAR(200)   ,
		@ClientCategoryId Int
	AS    
		BEGIN    

		UPDATE Clients SET [ClientCategoryId] = @ClientCategoryId 
		, ClientFullName= @ClientFullName
		WHERE ClientId = @Id 
	           
		END;    
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_Update_ClientCategores] 
		@Id INT,   
		@CategoryName NVARCHAR(200)
	AS    
		BEGIN    

		UPDATE ClientCategores SET  CategoryName= @CategoryName
		WHERE ClientCategoryId = @Id 
	           
		END;   
GO
