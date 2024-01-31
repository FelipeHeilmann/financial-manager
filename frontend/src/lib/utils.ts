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

export type TCreateInstallment = z.infer<typeof installmentZodSchema>

export type TTransaction = {
  id: string,
  amount: number,
  date: string,
  createdAt: string,
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