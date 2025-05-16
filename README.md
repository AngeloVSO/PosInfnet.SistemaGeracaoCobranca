# Sistema de Geração de Cobrança

Sistema desenvolvido em **.NET (C#)** para geração e validação de **cobranças via PIX e Boleto**. O objetivo principal é garantir a integridade dos dados da cobrança e a aplicação correta das regras de negócio conforme o perfil do cliente e o tipo de cobrança.

---

## Funcionalidades

- Geração de cobranças via **PIX** ou **Boleto**
- Validação de dados antes da geração da cobrança
- Controle de duplicidade por cliente
- Regras específicas por tipo de documento (CPF)
- Controle de validade e vencimento das cobranças

---

## Casos de Uso (com base nos testes)

### ✔️ PIX
- Gerar cobrança PIX válida com cliente e chave PIX corretos
- Exibir mensagem formatada com código PIX, valor e data de expiração
- Lançar exceção se:
  - O código PIX for inválido
  - A data de expiração for anterior à data de criação

### ✔️ Boleto
- Gerar cobrança Boleto válida com cliente e dados corretos
- Exibir mensagem com código de barras, valor e vencimento
- Lançar exceção se:
  - A data de vencimento for menor que a data de criação
  - O código de barras não tiver o número correto de dígitos

### ✔️ Domínio/Serviço de Validação
- Lançar exceção se:
  - Valor de cobrança para cliente com CPF exceder R$ 5.000
  - Já existir uma cobrança para o mesmo cliente (mesmo documento)
- Permitir geração se os dados forem válidos e únicos

---

## Regras de Negócio

1. **Limite por CPF**: Clientes com documento do tipo **CPF** não podem ter cobranças com valor superior a **R$ 5.000,00**.
2. **Cobrança única por cliente**: Não é permitido criar múltiplas cobranças para o mesmo cliente com o mesmo CPF.
3. **PIX**:
   - Deve conter uma chave PIX válida.
   - A **data de expiração** deve ser posterior à data de criação.
4. **Boleto**:
   - O **código de barras** deve ter um número específico de dígitos.
   - A **data de vencimento** deve ser posterior à data de criação.
