# BankOfBook
A proposta des repositório é criar uma api do zero ao docker, com bom banco de cache, nas próximas versões em outros repositórios veremos esta mesma api sendo usada em diversos contextos, mensageria rabbitmq, processos de CI e CD completos, husky, sonar e muito mais.
Funcionalidades deste repositório:

-> Api de busca de livros, criação e exclusão, alugar ou comprar. 
   -> Atualização da disponibilidade (alugado ou vendido)
   
-> Suporte a docker.

-> Suporte a logs elasticSearch + Kibana

-> Suporte a banco de dados de cache ou noSql (decidir).

-> Versionamento de API.

Vou adicionando aqui nos repositórios complementares com diversos meios de utilização.

-> Repositório (decidir) controle de inserção, atualização via mensageria Exchanges RabbitMq.

-> Repositório (decidir) Controle de pre commit and pre push (Husky) e Processos de CI e CD.
