create or replace procedure out_example(out i integer )
LANGUAGE 'plpgsql'
AS $$
begin
    i = 5 + 5;
    return;
end ;
$$;

call out_example(123123);