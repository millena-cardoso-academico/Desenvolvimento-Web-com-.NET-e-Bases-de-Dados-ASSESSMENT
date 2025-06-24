# Sistema de Gerenciamento para Agência de Turismo

Este projeto é uma aplicação web full-stack desenvolvida como avaliação para a disciplina de "Desenvolvimento Web com .NET e Bases de Dados". O sistema simula uma plataforma de gerenciamento para uma agência de turismo, permitindo o cadastro e controle de clientes, pacotes turísticos, reservas e destinos.

**Status do Projeto:** Concluído.

---

## 🚀 Funcionalidades Principais

O sistema implementa diversas funcionalidades essenciais para uma aplicação web moderna, demonstrando competências em back-end, front-end e banco de dados.

* **Gerenciamento de Clientes (CRUD):**
    * Criação, Leitura, Atualização e Exclusão de clientes.
    * Implementação de **Exclusão Lógica** (Soft Delete), onde os registros não são removidos fisicamente do banco, apenas marcados como inativos.

* **Gerenciamento de Pacotes:**
    * Cadastro de pacotes turísticos com informações como nome, preço, datas e vagas disponíveis.
    * Listagem e visualização de detalhes dos pacotes.

* **Autenticação e Autorização:**
    * Sistema de login e logout com credenciais definidas no código.
    * Proteção de rotas, onde páginas sensíveis (como o gerenciamento de clientes) só podem ser acessadas por usuários autenticados.

* **Lógica de Negócio Avançada:**
    * **Controle de Vagas:** Bloqueio automático de novas reservas quando a capacidade máxima de um pacote é atingida.
    * **Eventos e Delegates:** Uso de eventos em C# para notificar outras partes do sistema quando a capacidade de um pacote é esgotada.
    * **Serviço de Log:** Registro de operações importantes (como a criação de reservas) em um arquivo de texto (`log_operacoes.txt`).

* **Outras Entidades:**
    * CRUD básico para Cidades e Países de destino.

---

## 🛠️ Tecnologias Utilizadas

* **Back-end:** C# com .NET 8
* **Framework Web:** ASP.NET Core Razor Pages
* **Banco de Dados:** SQLite
* **ORM:** Entity Framework Core 8
* **Front-end:** HTML, CSS, JavaScript, Bootstrap

---

## ⚙️ Como Executar o Projeto

Siga os passos abaixo para configurar e rodar a aplicação em um ambiente de desenvolvimento local.

1.  **Pré-requisitos:**
    * [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
    * Visual Studio 2022 (ou um editor de código de sua preferência)

2.  **Clonar o Repositório:**
    ```bash
    git clone [https://github.com/millena-cardoso-academico/Desenvolvimento-Web-com-.NET-e-Bases-de-Dados-ASSESSMENT.git](https://github.com/millena-cardoso-academico/Desenvolvimento-Web-com-.NET-e-Bases-de-Dados-ASSESSMENT.git)
    cd Desenvolvimento-Web-com-.NET-e-Bases-de-Dados-ASSESSMENT
    ```

3.  **Abrir e Configurar o Banco de Dados:**
    * Abra o arquivo da solução (`.sln`) no Visual Studio.
    * Abra o **Console do Gerenciador de Pacotes** (Package Manager Console).
    * Execute o comando abaixo para aplicar as migrações e criar o banco de dados SQLite:
    ```powershell
    Update-Database
    ```

4.  **Executar a Aplicação:**
    * Pressione `F5` ou clique no botão de "play" (com o perfil `https` selecionado) para iniciar o projeto.
    * A aplicação estará disponível em `https://localhost:<PORTA>`.

---

## 🔑 Acesso ao Sistema

Para acessar as áreas protegidas da aplicação, utilize as seguintes credenciais:

* **Usuário:** `admin`
* **Senha:** `password`

---

## 👩‍💻 Autora

* **Millena Cardoso Sales Santos**
