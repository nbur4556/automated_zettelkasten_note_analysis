# Automated Zettelkasten Note Analysis

Analyzes the Zettelkasten criteria of a directory of notes.

## Local Development

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Ollama](https://ollama.com) running locally or accessible remotely

### Quick Start

1. Clone this repository
2. Copy the `run.sh.example` script and make executable:

```
cp run.sh.example run.sh
chmod +x run.sh
```
3. Update the environment variables inside your `run.sh` script with your local settings:

```
export OLLAMA_URI="http://localhost:11434"
export ANALYSIS_MODEL="clef:latest"
export CARDS_DIRECTORY="RELATIVE/OR/ABSOULTE/PATH/TO/NOTES/DIRECTORY"
```

4. Run the application: `./run.sh`
