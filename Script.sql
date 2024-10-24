create database db_DevReads;

use db_DevReads;

-- drop database db_DevReads;

create table tbCliente(
id int primary key auto_increment,
CPF decimal(11,0) unique not null,
NomeCli varchar(200) not null,
EmailCli varchar(30) not null,
SenhaCli int not null,
Tel int
);
-- fk_EmailCli varchar(30) not null 

create table tbNotaFiscal(
NF int primary key, -- Chave Principal NotaFiscal/Chave estrangeira tbCompra
TotalNota decimal(8,2) not null,
DataEmissao date not null
);
-- NF, TotalCompra, DataEmissao, fk_CPFCli -- NotaFiscal

/* O Auto_Incremente apenas deve ser usado uma vez.*/

create table tbLivro(
ISBN decimal(13,0) primary Key,
NomeLiv varchar(50) not null,
PrecoLiv decimal(6,2) not null,
DescLiv varchar(100)not null,
ImgLiv varchar(200) not null,
Categoria varchar(30) not null,
Autor varchar(50) not null,
DataPubli date not null,
Qtd int -- Chave Estrangeira da Tabela Editora
);
-- ISBN, NomeLiv, PrecoLiv, DescLiv, ImgLiv, Categoria, Autor, DataPubli, fk_CNPJ

create table tbEditora(
idEdi int primary key auto_increment,
CNPJ decimal (11,0) unique not null,
NomeEdi varchar(50) not null,
TelEdi int 
);
-- CNPJ, NomeEdi, TelEdi

create table tbCompra( 
NumeroCompra int primary key, -- Notafiscal
DataCompra date not null,
TotalCompra decimal(8,2) not null,
FormPag varchar(100) not null,
id_Cli int, -- Chave Estrangeira da Tabela tbCliente
NF int, -- Chave Estranheira nota fiscal
constraint FK_Id_Compra foreign key(Id_cli) references tbCliente(id),
constraint fk_NF foreign key(NF) references tbNotaFiscal(NF)
);
-- CodCompra, TotalCompra, QtdTotal, DataCompra, FormPag, fk_NF, fk_CPFCli_Compra

create table tbItemCompra(
NumeroCompra int,-- Chave Estrangeira CodCompra/NotaFiscal
ISBN decimal(13,0), -- Chave Estrangeira da tabela tbLivro
ValorItem decimal (8,2) not null,
Qtd int not null,
constraint primary key(NumeroCompra, ISBN),
constraint FK_NumeroCompra foreign key(NumeroCompra) references tbCompra(NumeroCompra),
constraint FK_ISBN_C foreign key(ISBN) references tbLivro(ISBN)
);
-- fk_NumeroCompra, fk_ISBNC, ValorItem, QtdC

create table tbVenda(
NumeroVenda int primary key,
DataVenda date not null,
ValorTotal decimal(8,2) not null,
QtdTotal int not null,
idEdi int, -- TbEditora
constraint fk_idEdi foreign key(idEdi) references tbEditora(idEdi)
);
-- CodVenda, DataVenda, ValorTotal, QtdTotal, fk_CNPJ

create table tbItemVenda(
NumeroVenda int, -- Chave estrangeira para a tabela tbVenda
ISBN decimal(13, 0) not null, -- Chave Estrangeira da tabela tbLivro
ValorItem decimal(8,2) not null,
Qtd int not null,
constraint PK_NV_ISBN primary key(NumeroVenda, ISBN),
constraint FK_NumeroVenda foreign key(NumeroVenda) references tbVenda(NumeroVenda),
constraint FK_ISBN foreign key(ISBN) references tbLivro(ISBN)
);
-- fk_NumeroVenda, fk_ISBNV, ValorItem, QtdV

/* Problema encontrado: Quando há duas chaves estrangeiras sendo apenas uma delas a chave primária,
A outra precisa ser indexada, ou seja, criar um index sobre ela para que possa se interligar a tabela que, 
no caso o fk_EmailCli da tbNotaFiscal ao Email da tbCliente. 
O problema é se isso já foi mostrado em aulas de Banco de Dados.*/

/* Mesmo problema: o id_Edi não é uma chave primária. Necessitando uma index */

-- Procedures! ----------------------------------------------------------------------------------
delimiter $$                  
create procedure spInsertCliente( vNomeCli varchar(200), vCPF decimal(11,0), vEmailCli varchar(30), vSenhaCli int, vTel int)
begin
if not exists (select CPF from tbCliente where CPF = vCPF)then
	insert into tbCliente(CPF, NomeCli, EmailCli, SenhaCli, Tel)
			values(vCPF, vNomeCli, vEmailCli, vSenhaCli, vTel);
else
select "Já tem";

end if;
end $$
call spInsertCliente('Niko', 46956936969, 'nikoolhate@gmail.com', 123456, 986754389);
call spInsertCliente('Luciano', 34567891011, 'Luciano@gmail.com', 132457, 997765421);
call spInsertCliente('Edu bolanhos', 34567665401, 'Edu@gmail.com', 345678, 934465421);
call spInsertCliente('Luciana Amelia Damasceno Ramos dos Santos', 34567665455, 'Luci@gmail.com', 345655, 934465455)

select * from tbCliente;

-- Procedure tbLivro ----------------------------------------------------------
delimiter $$                  
create procedure spInsertLivro(vISBN decimal(13,0), vNomeLiv varchar(50), vPrecoLiv decimal(6,2), 
vDescLiv varchar(100), vImgLiv varchar(200), vCategoria Varchar(30), vAutor varchar(50), vDataPubli char(10), vQtd int)
begin
if not exists (select ISBN from tbLivro where ISBN = vISBN)then
	insert into tbLivro(ISBN, NomeLiv, PrecoLiv, DescLiv, ImgLiv, Categoria, Autor, DataPubli, Qtd)
			values(vISBN, vNomeLiv, vPrecoLiv, vDescLiv, vImgLiv, vCategoria, vAutor, str_to_date(vDataPubli, '%d/%m/%Y'), vQtd);

else
select "Já tem!";

end if;
end $$

call spInsertLivro(9788576082675, 'Código Limpo', 85.00, 'Habilidades da codificação de software',
'CodigoLimpo.jpg', 'FrontEnd', 'Robert Cecil Martin', '19/06/2012', 10);
call spInsertLivro(9788535262128, 'Como Criar Uma Mente', 65.00, 'Conhecimento da tecnologia para com a mente humana',
'ComoCriar.jpg','Inteligência Artificial e Machine Learning', 'Ray Kurzweil', '13/11/2012', 10);
call spInsertLivro(, '', , '', '', '', '', '', );
call spInsertLivro(, '', , '', '', '', '', '', );
call spInsertLivro(, '', , '', '', '', '', '', );
call spInsertLivro(, '', , '', '', '', '', '', );
call spInsertLivro(, '', , '', '', '', '', '', );
call spInsertLivro(, '', , '', '', '', '', '', );

-- Acrescentar a editora nos livros (atributo) - ORDEM DAS EDITORAS DOS LIVROS A SEGUIR SEGUE A ORDEM DOS INSERTS DOS LIVROS
/*
Alta Books
Companhia das Letras
Érica
Visual Books
Matrix Editora
Editora Gente
Alta Books
*/

select * from tblivro;

-- Procedure compra
delimiter $$
Create procedure spInsertCompra
(vNumeroCompra int, vISBN decimal(13, 0), vQtd int, vNomeCli varchar(200), vValorItem decimal(8, 2), vFormPag varchar(40))
begin
    declare vIdCli int;
    select Id into vIDCli from tbCliente where NomeCli = vNomeCli;
        if exists (select NomeCli from tbCliente where NomeCli = vNomeCli) and
         exists (select ISBN from tbLivro where ISBN = vISBN) then
           
           insert into tbCompra (NumeroCompra, DataCompra, TotalCompra, FormPag, ID_Cli)
				values (vNumeroCompra, current_date(), (vValorItem * vQtd), vFormPag, vIdCli);
            
            insert into tbItemCompra (NumeroCompra, ISBN, ValorItem, Qtd)
				values (vNumeroCompra, vISBN, vValorItem, vQtd);
        end if;
       
end $$

call spInsertCompra(3, 1234567891023, 3, 'Edu bolanhos', 85.00, 'Dinheiro');
call spInsertCompra(2, 1234567891023, 2, 'Luciano', 85.00, 'Pix');
call spInsertCompra(1, 1234567891023, 1, 'Niko', 85.00, 'Cartão');
call spInsertCompra(4, 1234567891023, 4, 'Luciana Amelia Damasceno Ramos dos Santos', 85.00, 'débito');
select * from tbCompra;

-- Venda ------------------------------------------------------------------------------------
delimiter $$
Create procedure spInsertVenda(vNumeroVenda int, vNomeEdi varchar(200), vDataVenda char(10), vISBN decimal(13,0), vValorItem decimal (8,2), vQtd int, vQtdTotal int, vValorTotal decimal (8,2))
BEGIN 
	If not exists (select NumeroVenda from tbVenda where  NumeroVenda = vNumeroVenda) then
		If exists (select idEdi from tbEditora where NomeEdi = vNomeEdi) and exists (select ISBN from tbLivro where ISBN = vISBN) then
			insert into tbVenda (NumeroVenda, DataVenda, ValorTotal, QtdTotal,idEdi) 
				values (vNumeroVenda, str_to_date(vDataVenda, '%d/%m/%Y'), vValorTotal, vQtdTotal, (select idEdi from tbEditora where NomeEdi = vNomeEdi));
		End if;
	End if; 
	
    If not exists (select * from tbItemVenda where (ISBN = vISBN) and (NumeroVenda = vNumeroVenda)) then
		insert into tbItemVenda (NumeroVenda, ISBN, ValorItem, Qtd)
			values (vNumeroVenda, vISBN, vValorItem, vQtd);
	End if;
END $$

call spInsertVenda(8459, 'Editora Dialética', '01/05/2018', 1234567891023, 22.22, 200, 700, 21944.00);

describe tbVenda;
describe tbItemvenda;
select * from tbitemVenda;

-- Editora ----------------------------------------------------------------
describe tbEditora;
delimiter $$                  
create procedure spInsertEditora(vCNPJ decimal(11,0), vNomeEdi varchar(50), vTelEdi int)
begin
if not exists (select CNPJ from tbEditora where CNPJ = vCNPJ)then
	insert into tbEditora(CNPJ, NomeEdi, TelEdi)
			values(vCNPJ, vNomeEdi, vTelEdi);
else
select "Já tem";

end if;
end $$

call spInsertEditora (56978912330, 'Editora Dialética', 987654321);
select * from 
-- NotaFiscal

delimiter $$
create procedure spInsertNF(vNF int, vNomeCli varchar(200))
begin
declare vTotalNota decimal(8,2);
if exists(select vNomeCli from tbCliente where NomeCli = vNomeCli)then
	if not exists (select NF from tbNotaFiscal where NF = vNF) then
    
    set vTotalNota = (select sum(TotalCompra) from tbCompra where id_Cli = (select id from tbCliente where Nomecli = vNomecli));
    
		insert into tbNotaFiscal(NF, TotalNota, DataEmissao)
			values(vNF, vTotalNota, current_date());

	end if;
end if;
end $$

call spInsertNF (359, 'Niko'); 
call spInsertNF (360, 'Luciano'); 
call spInsertNF (361, 'Edu bolanhos'); 

select * from tbNotaFiscal;

-- Triggers! -------------------------------------------------------------------------------
-- select * from tbClienteHistorico; 
-- Select * from tbCliente;
-- drop procedure spInsertCliente;
-- drop table tbClienteHistorico;
 describe tbCliente;

create table tbClienteHistorico like tbCliente; -- Teste de Histórico
alter table tbClienteHistorico add Ocorrencia varchar(20) NULL AFTER Tel;
alter table tbClienteHistorico add Atualizacao datetime null after Ocorrencia;

DELIMITER $$
create trigger trgClienteNovo AFTER INSERT ON tbCliente
for each row
begin
    insert into tbClienteHistorico (CPF, NomeCli, EmailCli, SenhaCli, Tel, Ocorrencia, Atualizacao)
    values (NEW.CPF, NEW.NomeCli, NEW.EmailCli, NEW.SenhaCli, NEW.Tel, 'Novo', NOW());
end$$

call spInsertCliente(46956936969,'Niko', 'nikoolhate@gmail.com' , 123456, 986754389);

-- show create trigger trgClienteNovo;
-- show create procedure spInsertCliente;
-- show create table tbCliente;

-- Livro ////////////////////////////////////////////////////////
create table tbLivroHistorico like tbLivro; -- Teste de Histórico
alter table tbLivroHistorico add Ocorrencia varchar(20) NULL AFTER DataPubli;
alter table tbLivroHistorico add Atualizacao datetime null after Ocorrencia;

describe tbLivro;

DELIMITER $$
create trigger trgLivroNovo AFTER INSERT ON tbLivro
for each row
begin
    insert into tbLivroHistorico (ISBN, NomeLiv, PrecoLiv, DescLiv, ImgLiv, Categoria, Autor, DataPubli, Ocorrencia, Atualizacao)
    values (NEW.ISBN, NEW.NomeLiv, NEW.PrecoLiv, NEW.DescLiv, NEW.ImgLiv, NEW.Categoria, NEW.Autor, NEW.Datapubli, 'Novo', NOW());
end$$
select * from tbcomprahistorico;

-- Compra /////////////////////////////////////////////////////////
create table tbCompraHistorico like tbCompra; -- Teste de Histórico
alter table tbCompraHistorico add Ocorrencia varchar(20) NULL AFTER id_Cli;
alter table tbCompraHistorico add Atualizacao datetime null after Ocorrencia;
describe tbCompra;

DELIMITER $$
create trigger trgCompraNova AFTER INSERT ON tbCompra
for each row
begin
    insert into tbCompraHistorico (NumeroCompra, DataCompra, TotalCompra, FormPag, id_Cli, NF, Ocorrencia, Atualizacao)
    values (NEW.NumeroCompra, NEW.DataCompra, NEW.TotalCompra, NEW.FormPag, NEW.id_Cli, NEW.NF, 'Novo', NOW());
end$$

-- Venda ///////////////////////////////////////////////////////////
create table tbVendaHistorico like tbVenda; -- Teste de Histórico
alter table tbVendaHistorico add Ocorrencia varchar(20) NULL AFTER idEdi;
alter table tbVendaHistorico add Atualizacao datetime null after Ocorrencia;

describe tbVenda;
DELIMITER $$
create trigger trgVendaNova AFTER INSERT ON tbVenda
for each row
begin
    insert into tbVendaHistorico (CodVenda, DataVenda, ValorTotal, QtdTotal, idEdi, Ocorrencia, Atualizacao)
    values (NEW.CodVenda, NEW.DataVenda, NEW.ValorTotal, NEW.QtdTotal, New.idEdi, 'Novo', NOW());
end$$

-- Editora ///////////////////////////////////////////////////////
create table tbEditoraHistorico like tbEditora; -- Teste de Histórico
alter table tbEditoraHistorico add Ocorrencia varchar(20) NULL AFTER TelEdi;
alter table tbEditoraHistorico add Atualizacao datetime null after Ocorrencia;

describe tbEditora;
DELIMITER $$
create trigger trgEditoraNova AFTER INSERT ON tbEditora
for each row
begin
    insert into tbVendaHistorico (idEdi, CNPJ, NomeEdi, TelEdi, Ocorrencia, Atualizacao)
    values (NEW.idEdi, NEW.CNPJ, NEW.NomeEdi, NEW.TelEdi, 'Novo', NOW());
end$$

-- NotaFiscal //////////////////////////////////////////////////////////////////
describe tbNotafiscal;

create table tbNotaFiscalHistorico like tbNotaFiscal; -- Teste de Histórico
alter table tbNotaFiscalHistorico add Ocorrencia varchar(20) NULL AFTER IdCli;
alter table tbNotaFiscalHistorico add Atualizacao datetime null after Ocorrencia;
select * from tbCompraHistorico;
DELIMITER $$
create trigger trgNotaFiscalNova AFTER INSERT ON tbNotaFiscal
for each row
begin
    insert into tbNotaFiscalHistorico (NF, TotalCompra, DataEmissao, idCli, Ocorrencia, Atualizacao)
    values (NEW.NF, NEW.TotalCompra, NEW.DataEmissao, NEW.idCli, 'Novo', NOW());
end$$

-- Procedure de Select na Venda, Editora e... Produto/Livro.
-- Adicionar Chaves Estrangeiras às Tabelas
-- Criar Procedures nas Tabelas Importantes
-- Criar Triggers/Tabelas-Históricos para registrar as informações de compra, venda, etc.
-- Engenharia Reversa, provando o modelo lógico.
