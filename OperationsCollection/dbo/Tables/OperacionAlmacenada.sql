CREATE TABLE [dbo].[OperacionAlmacenada] (
    [OperationID] INT            IDENTITY (1, 1) NOT NULL,
    [Operacion]   NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_OperacionAlmacenada] PRIMARY KEY CLUSTERED ([OperationID] ASC)
);

