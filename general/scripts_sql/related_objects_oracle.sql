select name
        , type
        , referenced_name
        , referenced_type
  from all_dependencies
  where referenced_name like 'EMP'
  and   referenced_owner = 'SCOTT';
  
  SELECT * FROM
  all_dependencies a
  WHERE upper(a.REFERENCED_NAME) LIKE UPPER('%LOG_ORDEM_SERVICO%');
  

  
  
