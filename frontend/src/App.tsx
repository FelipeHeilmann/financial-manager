import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import axios from "axios"
import { ChevronDown } from "lucide-react"
import { useEffect, useState } from "react"

type TTransaction = {
  id: string,
  amount: number,
  date: string,
  createdAt: string,
  author: string,
  type: number,
  installments: TInstallment[]
}

type TInstallment = {
  id: string,
  amount: number,
  transactionId: string,
  date: string,
  createdAt: string
}

export const timestampToUser = (date: string) => {
  const passedDate = new Date(date)
  const passedYear = String(passedDate.getFullYear()).slice(2)
  const passedMonth = String(passedDate.getMonth() + 1).padStart(2, '0')
  const passedDay = String(passedDate.getDate()).padStart(2, '0')
  return `${passedDay}-${passedMonth}-${passedYear}`
}

export default function App() {
  const [total, setTotal] = useState(0)
  const [transactions, setTransactions] = useState<TTransaction[]>([])
  const [expandedStates, setExpandedStates] = useState(transactions.map(() => false))

  const toggleExpansion = (index: number) => {
    const newExpandedStates = [...expandedStates]
    newExpandedStates[index] = !newExpandedStates[index]
    setExpandedStates(newExpandedStates)
  }

  const fetchData = async () => {
    const { data } = await axios.get("http://localhost:5058/api/transactions") as { data: TTransaction[] }

    let totalTransaction = 0
    for (const transaction of data) {
      if (transaction.type === 1) totalTransaction -= transaction.amount
      if (transaction.type === 2) totalTransaction += transaction.amount
    }

    setTotal(totalTransaction)
    setTransactions(data)
  }

  useEffect(() => {
    fetchData()
  }, [])
  return (
    <main className="w-[100vw] h-[100vh] bg-black bg-opacity-95 space-y-4">
      <section className="w-[30%] mx-auto py-5 rounded-lg border border-white p-2">
        <h1 className="text-white text-xl font-bold">Total disponivel : <span className={`${total > 0 ? 'text-green-500' : 'text-red-500'} `}>{total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</span></h1>
      </section>
      <section className="w-full flex flex-wrap px-10">
          {
            transactions.map((transaction,index) =>
              <Card key={transaction.id} className="w-[25%] bg-slate-900 bg-opacity-70 border-0 text-white">
                <CardHeader>
                  <CardTitle>
                    {transaction.type === 1 ? "Crédito" : "Débido"}
                  </CardTitle>
                </CardHeader>
                <CardContent className="space-y-3">
                  <div className="space-y-2">
                    <div className="flex justify-between">
                      <p className="text-lg font-semibold">{transaction.author}</p>
                      <p className={`text-lg font-semibold ${transaction.type === 1 ? 'text-red-600' : 'text-green-500'}`}>{transaction.amount.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
                      <p className="text-lg font-semibold">{timestampToUser(transaction.date)}</p>
                    </div>
                    <div>
                      <p className="text-lg font-semibold">Restante a pagar: R$ {transaction.amount - transaction.installments.reduce((acc,cur) => acc + cur.amount, 0)}</p>
                    </div>
                    <div className="flex justify-end">
                      <button onClick={() => toggleExpansion(index)} className="bg-white rounded-full flex justify-center items-center">
                        <ChevronDown color="#000" />
                      </button>
                    </div>
                  </div>
                  <div className={`space-y-2 transition-all overflow-hidden ${ expandedStates[index] ? 'max-h-80' : 'max-h-0'}`}>
                    {
                      transaction.installments.map((installment, index) =>
                        <div className="w-full bg-slate-800 p-2 rounded-lg space-y-2">
                          <h2 className="font-bold text-xl">Parcela {index + 1}</h2>
                          <p className="text-lg font-semibold">{installment.amount.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
                          <p className="text-lg font-semibold">{timestampToUser(installment.date)}</p>
                        </div>
                      )
                    }
                  </div>
                </CardContent>
              </Card>
            )
          }
      </section>
    </main >
  )
}
