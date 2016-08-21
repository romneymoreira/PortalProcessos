CREATE TABLE Setores(
	IdSetor int IDENTITY(1,1) NOT NULL,
	NomeSetor varchar(100) NOT NULL
);

CREATE TABLE ProcessoAtividade(
	Id int IDENTITY(1,1) NOT NULL,
	IdSetor int NOT NULL,
	Atividade varchar(max) NOT NULL,
	Tipo int NOT NULL,
	Responsavel varchar(100) NOT NULL,
	DataSolicitacao datetime NOT NULL,
	Status int NOT NULL,
	Observacao varchar(max) NOT NULL,
	FOREIGN KEY (IdSetor) REFERENCES Setores(IdSetor)
);

CREATE TABLE VisitasPortal(
	Id int IDENTITY(1,1) NOT NULL,
	IdUser int NOT NULL,
	DataVisita datetime NOT NULL,
	FOREIGN KEY (IdUser) REFERENCES [User](Id)
);



CREATE TABLE LinksPortal(
	IdLink int IDENTITY(1,1) NOT NULL,
	IdSetor int NOT NULL,
	UrlLink varchar(300) NOT NULL,
	DataInclusao datetime NOT NULL,
	NomeLink varchar(50) NOT NULL,
	FOREIGN KEY (IdSetor) REFERENCES Setores(IdSetor)
);

CREATE TABLE AcessosPortal(
	IdAcesso int IDENTITY(1,1) NOT NULL,
	IdUser int NOT NULL,
	DataAcesso datetime NOT NULL,
	FOREIGN KEY (IdUser) REFERENCES [User](Id)
);

insert into Setores values('Administração');
insert into Setores values('Benefícios');
insert into Setores values('Contabilidade');
insert into Setores values('Empréstimos');
insert into Setores values('Institucional');
insert into Setores values('Investimentos');
insert into Setores values('Organizacional');
insert into Setores values('Recepção');
insert into Setores values('RPA');
insert into Setores values('Tesouraria');