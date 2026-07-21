import { useState } from 'react';
import ChatWindow from './components/ChatWindow';
import type { ChatMessage } from './components/ChatWindow';
import ChatInput from './components/ChatInput';
import { askQuestion } from './services/api';

function App() {
  const [messages, setMessages] = useState<ChatMessage[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const handleSend = async (content: string) => {
    const userMessage: ChatMessage = { role: 'user', content };
    setMessages((previous) => [...previous, userMessage]);
    setIsLoading(true);

    try {
      const response = await askQuestion(content);
      const assistantMessage: ChatMessage = {
        role: 'assistant',
        content: response.answer,
      };
      setMessages((previous) => [...previous, assistantMessage]);
    } catch {
      const errorMessage: ChatMessage = {
        role: 'assistant',
        content: 'Sorry, something went wrong while contacting the log analyzer service.',
      };
      setMessages((previous) => [...previous, errorMessage]);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="app-shell">
      <div className="chat-card">
        <header className="chat-header">
          <h1>IT Helpdesk Log Analyzer</h1>
        </header>

        <ChatWindow messages={messages} isLoading={isLoading} />

        <ChatInput isLoading={isLoading} onSend={handleSend} />
      </div>
    </div>
  );
}

export default App;
