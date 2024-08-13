create or replace procedure fresherCG(sem IN int)
is
    n int;
begin
    if sem=1 then
        update student set cgpa=10 where semester=sem;
    end if;
end;
/
