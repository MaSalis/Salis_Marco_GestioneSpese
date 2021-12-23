create database GestioneSpese
--drop database GestioneSpese
--drop table Categoria
--drop table Spesa

create table Categoria (
CategoriaId int identity(1,1),
Categoria varchar(100) not null,
constraint PK_Categoria primary key (CategoriaId)
)
create table Spesa (
SpesaId int identity(1,1),
DataSpesa datetime not null,
Descrizione varchar(500) not null,
Utente varchar(100) not null,
Importo decimal(4,2) not null,
Approvato bit not null,
constraint PK_Spesa primary key (SpesaId),
CategoriaId int foreign key references Categoria(CategoriaId)
)
insert into Categoria values('Categ1');
insert into Categoria values('Categ2');
insert into Categoria values('Categ3');
insert into Categoria values('Categ4');
insert into Categoria values('Categ5');
insert into Spesa values('2021-12-12', 'spesa1', 'Marco', 12.2 , 0, 1);
insert into Spesa values('2021-12-13', 'spesa2', 'Marco', 5 , 0, 2);

--Queries di prova
select * from Spesa where Approvato = 1

select * from Spesa where Utente = 'Marco'

select c.Categoria, sum(s.Importo) as tot
from Spesa s join Categoria c on s.CategoriaId = c.CategoriaId
where c.Categoria = 'Categ1'
group by c.Categoria