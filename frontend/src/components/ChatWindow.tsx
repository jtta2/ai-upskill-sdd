import { useEffect, useRef } from 'react';

export interface ChatMessage {
  role: 'user' | 'assistant';
  content: string;
}

interface ChatWindowProps {
  messages: ChatMessage[];
  isLoading: boolean;
}

function ChatWindow({ messages, isLoading }: ChatWindowProps) {
  const bottomRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [messages, isLoading]);

  return (
    <div className="chat-window">
      {messages.length === 0 && !isLoading && (
        <div className="chat-empty-state">
          Ask a question about your logs to get started.
        </div>
      )}

      {messages.map((message, index) => (
        <div
          key={index}
          className={`chat-bubble-row chat-bubble-row--${message.role}`}
        >
          <div className={`chat-bubble chat-bubble--${message.role}`}>
            {message.content}
          </div>
        </div>
      ))}

      {isLoading && (
        <div className="chat-bubble-row chat-bubble-row--assistant">
          <div className="chat-bubble chat-bubble--assistant chat-bubble--typing">
            <span className="chat-typing-dot" />
            <span className="chat-typing-dot" />
            <span className="chat-typing-dot" />
          </div>
        </div>
      )}

      <div ref={bottomRef} />
    </div>
  );
}

export default ChatWindow;
