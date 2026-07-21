import { useState } from 'react';
import type { FormEvent, KeyboardEvent } from 'react';

interface ChatInputProps {
  isLoading: boolean;
  onSend: (message: string) => void;
}

function ChatInput({ isLoading, onSend }: ChatInputProps) {
  const [text, setText] = useState('');

  const submitMessage = () => {
    const trimmed = text.trim();
    if (!trimmed || isLoading) {
      return;
    }

    onSend(trimmed);
    setText('');
  };

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    submitMessage();
  };

  const handleKeyDown = (event: KeyboardEvent<HTMLTextAreaElement>) => {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      submitMessage();
    }
  };

  return (
    <form className="chat-input-bar" onSubmit={handleSubmit}>
      <textarea
        className="chat-input-textarea"
        value={text}
        placeholder="Ask about your logs..."
        rows={1}
        disabled={isLoading}
        onChange={(event) => setText(event.target.value)}
        onKeyDown={handleKeyDown}
      />
      <button
        type="submit"
        className="chat-send-button"
        disabled={isLoading || !text.trim()}
      >
        Send
      </button>
    </form>
  );
}

export default ChatInput;
