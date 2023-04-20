CREATE TABLE [dbo].[Store] (
    [OperationID] INT            IDENTITY (1, 1) NOT NULL,
    [Operacion]   NCHAR (255)    NOT NULL,
    [Resultado]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED ([OperationID] ASC)
);

