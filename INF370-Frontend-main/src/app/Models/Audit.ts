export interface Audit {
    id: number;
    timestamp: Date;
    userId: string;
    action: string;
    amount: number;
    quantity: number;
  }
  