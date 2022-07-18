create or replace procedure increase(inout i integer )
LANGUAGE 'plpgsql'
AS $$
begin
    i = i+1;
    return;
end ;
$$;

call increase(1);