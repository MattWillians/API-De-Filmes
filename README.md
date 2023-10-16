# API FilmesApi

Bem-vindo à documentação da API FilmesApi. Esta API é usada para gerenciar informações sobre filmes em um banco de dados. Abaixo estão os métodos disponíveis e suas descrições:

## POST /FilmesApi

Este método é usado para adicionar um novo filme ao banco de dados.

**Exemplo de Requisição:**

```http
POST /FilmesApi
Content-Type: application/json

{
  "titulo": "Nome do Filme",
  "diretor": "Nome do Diretor",
  "ano": 2023,
  "genero": "Ação"
}
```

## GET /FilmesApi

Este método é responsável por recuperar todos os filmes da lista de filmes dentro do banco de dados.

## GET /FilmesApi/{id}
Este método busca um filme com base na sua chave de acesso única.

**Exemplo de Requisição:**

```http
GET /FilmesApi/1
```

## PUT /FilmesApi/{id}

O método PUT é usado para atualizar TODAS as informações de um filme com base no seu ID único.

**Exemplo de Requisição:**

```http
PUT /FilmesApi/1
Content-Type: application/json

{
  "titulo": "Novo Título do Filme",
  "diretor": "Novo Diretor do Filme",
  "ano": 2024,
  "genero": "Comédia",
}
```

## PATCH /FilmesApi/{id}

O método PATCH é usado para atualizar UMA ou mais informações do filme em questão com base no seu ID único.

**Exemplo de Requisição:**

```http
PATCH /FilmesApi/1
Content-Type: application/json
[
	{
  		"genero": "Drama"
	}
]
```

DELETE /FilmesApi/{id}
Este método é usado para deletar um filme com base no seu ID.

**Exemplo de Requisição:**

```http
DELETE /FilmesApi/1
```


# Para mais informações, use a documentação do SWAGGER
