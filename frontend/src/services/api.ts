const API_BASE_URL = 'http://localhost:5000';

export interface ChatRequest {
  message: string;
}

export interface ChatResponse {
  answer: string;
}

export async function askQuestion(message: string): Promise<ChatResponse> {
  const response = await fetch(`${API_BASE_URL}/api/chat/ask`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({ message } satisfies ChatRequest),
  });

  if (!response.ok) {
    throw new Error(`Chat request failed with status ${response.status}`);
  }

  return (await response.json()) as ChatResponse;
}
