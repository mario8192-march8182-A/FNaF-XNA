# Five Nights at Freddy's - XNA Framework

Um jogo FNaF (Five Nights at Freddy's) implementado usando o **Microsoft XNA Framework** com suporte a Xbox Live.

## 📁 Estrutura do Projeto

```
FNaF-XNA/
├── Program.cs                 # Ponto de entrada principal
├── AppMain.cs                 # Classe principal que herda de Game
├── Game1.cs                   # Lógica principal do jogo
├── Properties/
│   └── AssemblyInfo.cs       # Metadados da assemblagem
├── Systems/
│   ├── AnimatronicSystem.cs  # Sistema de AI dos animatrônicos
│   ├── CameraSystem.cs       # Sistema de câmera
│   ├── AudioSystem.cs        # Sistema de áudio
│   ├── UISystem.cs           # Sistema de interface do usuário
│   └── XBOXLive.cs          # Integração Xbox Live (Opcional)
├── Microsoft/Xna/
│   └── Framework/
│       └── README.md         # Documentação do XNA Framework
└── README.md                 # Este arquivo
```

## 🎮 Componentes Principais

### Program.cs
- Ponto de entrada do aplicativo
- Inicializa e executa o loop do jogo

### AppMain.cs
- Herda de `Microsoft.Xna.Framework.Game`
- Gerencia o ciclo de vida da aplicação
- Integra todos os sistemas do jogo

### Game1.cs
- Lógica principal do gameplay
- Gerencia estados do jogo (Menu, Playing, GameOver, Won, Paused)
- Controla o timer noturno
- Implementa a física do jogo

### Sistemas Implementados

#### AnimatronicSystem
- Gerencia o comportamento e IA dos animatrônicos
- Controla agressividade baseada no progresso da noite
- Movimento dinâmico dos personagens

#### CameraSystem
- Sistema de câmera para visualizar diferentes áreas
- Suporte a zoom e pan
- Conversão entre coordenadas mundiais e de tela

#### AudioSystem
- Reprodução de efeitos sonoros
- Gerenciamento de música de fundo
- Controle de volume mestre

#### UISystem
- Gerenciamento de elementos de interface
- Botões, labels e outros componentes UI
- Renderização de HUD e menus

#### XBOXLive (Opcional)
- Integração com Xbox Live
- Gerenciamento de achievements
- Salvar na nuvem
- Leaderboard

## 🚀 Como Usar

### Requisitos
- Visual Studio 2010 ou superior
- XNA Game Studio 4.0
- .NET Framework 4.0+

### Compilação
```bash
msbuild FNaF-XNA.csproj /p:Configuration=Release
```

### Execução
```bash
FNaF-XNA.exe
```

### Controles do Jogo
- **ENTER** - Iniciar jogo
- **P** - Pausar/Resumir
- **R** - Reiniciar
- **ESC** - Ir para menu
- **Movimento do Mouse** - Câmera
- **Scroll do Mouse** - Zoom

## 🎯 Recursos Implementados

- ✅ Loop principal do jogo XNA
- ✅ Sistema de estados do jogo
- ✅ IA de animatrônicos
- ✅ Sistema de câmera
- ✅ Gerenciamento de áudio
- ✅ UI interativa
- ✅ Integração Xbox Live (template)
- ✅ Estrutura modular e extensível

## 🔧 Extensões Futuras

- [ ] Carregar texturas dos animatrônicos
- [ ] Implementar câmeras de segurança
- [ ] Sistema de porta e luz
- [ ] Cutscenes animadas
- [ ] Salvar progresso
- [ ] Múltiplos níveis de dificuldade
- [ ] Ativação completa do Xbox Live

## 📚 Referências

- [Microsoft XNA Documentation](https://en.wikipedia.org/wiki/XNA)
- [XNA Creator's Club](https://www.creators.xna.com/)
- [Monogame (Sucessor do XNA)](https://www.monogame.net/)

## 📝 Notas

- Este é um template/framework para FNaF em XNA
- Adequado para Windows e Xbox 360
- Usa Direct3D para renderização
- Suporta multiplos controladores

## 📄 Licença

Este projeto é fornecido como template educacional.

## 👥 Contribuições

Contribuições são bem-vindas! Sinta-se livre para melhorar o código e adicionar novos recursos.

---

**Desenvolvido com ❤️ usando XNA Framework**
