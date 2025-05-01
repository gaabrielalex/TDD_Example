# TDD Example

Este é um projeto de exemplo que demonstra a implementação de Test-Driven Development (TDD) em C# usando MSTest.

## Estrutura do Projeto

O projeto está organizado nas seguintes camadas:

- **Domain**: Contém as classes de modelo e validações
  - PessoaModel: Modelo que representa uma pessoa com Nome, CPF, Celular e Data de Nascimento
  - Validators: Validadores usando FluentValidation

- **Utils**: Classes utilitárias
  - CPF: Validação de CPF
  - Phone: Validação de números de telefone

- **DomainTest**: Testes unitários
  - Testes de validação do PessoaModel
  - Cenários de teste usando data-driven tests

## Tecnologias Utilizadas

- .NET Framework 4.8.1
- MSTest para testes unitários
- FluentValidation para validações
- Moq para mocking em testes
- FluentAssertions para assertions mais legíveis

## Exemplos de Validações Implementadas

- Validação de CPF (formato e dígitos verificadores)
- Validação de número de telefone (formato brasileiro)
- Validação de data de nascimento (não pode ser futura)
- Validações de campos obrigatórios
