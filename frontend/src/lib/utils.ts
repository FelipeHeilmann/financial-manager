import axios from "axios"
import { type ClassValue, clsx } from "clsx"
import { twMerge } from "tailwind-merge"
import z from "zod"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export const timestampToUser = (date: string) => {
  const passedDate = new Date(date)
  const passedYear = String(passedDate.getFullYear()).slice(2)
  const passedMonth = String(passedDate.getMonth() + 1).padStart(2, '0')
  const passedDay = String(passedDate.getDate()).padStart(2, '0')
  return `${passedDay}-${passedMonth}-${passedYear}`
}

export const axiosInstance = axios.create({
  baseURL: 'http://localhost:5058'
})

export const installmentZodSchema = z.object({
  amount: z.coerce.number(),
  date: z.any()
})

export const transactionZodSchema = z.object({
  amount: z.coerce.number(),
  date: z.string(),
  author: z.string(),
  type: z.number(),
  description: z.string()
})

export type TCreateTransaction = z.infer<typeof transactionZodSchema>
export type TCreateInstallment = z.infer<typeof installmentZodSchema>

export type TTransaction = {
  id: string,
  amount: number,
  date: string,
  createdAt: string,
  description: string
  author: string,
  type: number,
  installments: TInstallment[]
}

export type TInstallment = {
  id: string,
  amount: number,
  transactionId: string,
  date: string,
  createdAt: string
}

export const ErrorsDictionary = {
  ["Installment.invalid.amount"]: "Valor da parcela maior que o disponível para pagar",
  ["Installment.Amount.Less.Equal.Zero"]: "O Valor da parcela deve ser maior que zero",
  ["Installment.Not.Found"]: "Parcela não encontrada",

  ["Cannot.Add.Installment"]: "Transação de débito não pode ter parcela",
  ["Invalid.Transaction.Type"]: "Tipo de transação inválido",
  ["Transaction.Not.Found"]: "Transação não encontrada",

}