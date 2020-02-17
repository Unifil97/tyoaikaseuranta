create proc nakyma
@Id int
as
	begin
	select *
	from Tyot
	where Id=@Id
	end

	
	

