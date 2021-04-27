# Gestão de Projetos

Gestão de Projetos é um programa feito em C# para controle de projetos e sua porcentagem de completação, incluindo itens como textos, imagens e gráficos.

---

## Como funciona?

O programa conta com as seguintes telas e funcionalidades:

### - CARREGAMENTO
Aqui é feito o carregamento e atribuição de projetos salvos no sistema do usuário na tela de menu.

### - MENU
Nessa tela ocorre a criação de projetos, sendo as seguintes opções disponíveis ao seleciona-los:
- Abrir, onde é aberta a tela do projeto, em sua forma padrão, com todas as funcionalidades;
- Editar, onde é exibido um overlay para edição do nome;
- Salvar, sendo realizado o salvamento dos dados do projeto em um arquivo XML;
- Excluir, sendo excluído o projeto do programa, e caso salvo, seu arquivo XML correspondente.

### - PROJETO

É aqui onde os conteúdos como textos, imagens e gráficos são adicionados e gerenciados, sendo, ao selecionados, exibidos as seguintes opções:
- Editar, sendo exibido um overlay correspondente ao item selecionado;
- Excluir, onde o item é excluído do projeto;
- Mover, onde o usuário poderá mover o item para ANTES ou DEPOIS de outro da lista de itens adicionados;

Essa tela possui a seguinte variante:

#### - SEÇÃO

Sendo a tela exibida ao abrir projetos ou outras seções, tem as funções da tela de projeto e possibilita a adição de seções e tópicos dentro da mesma!

Semelhante aos tópicos, elas possuem uma porcentagem, porém diferentemente deles, ela é dada pela média de seus tópicos e seções.

Seções e tópicos adicionados possuem a opção extra [ABRIR], onde é aberta uma nova tela para o item selecionado.

---

Ao fechar uma tela, a tela anterior (ou origem), será aberta em seu lugar. Isso não é válido para a tela de menu, sendo exibido um prompt para confirmação de saída do programa.

---

## Como usar?

Use os botões no canto inferior direito da tela para controle de projetos e outros itens!
