insert into Table_User(UserName,UserPassword) values ('admin','admin123')
go

select * from Table_User
go

create trigger AddIDForProfile
on Table_User for Insert
as
declare @UserId int 
begin
select @UserId = UserId from inserted
insert into UserProfile(UserId) values (@UserId)
end





