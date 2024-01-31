import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { useEffect, useState } from "react"
import { TTransaction, axiosInstance, timestampToUser } from "./lib/utils"
import { DeleteTransactionModal, InstallmentModal } from "./components/modal"
import InstallmentComponent from "./components/installment-component"
import { Toaster } from "./components/ui/toaster"


export default function App() {
  const [total, setTotal] = useState(0)
  const [transactions, setTransactions] = useState<TTransaction[]>([])

  async function fetchData() {
    const { data } = await axiosInstance.get("/api/transactions") as { data: TTransaction[] }

    let totalTransaction = 0
    for (const transaction of data) {
      if (transaction.type === 1) totalTransaction -= transaction.amount
      if (transaction.type === 2) totalTransaction += transaction.amount
      totalTransaction += transaction.installments.reduce((acc, cur) => acc + cur.amount, 0)
    }
    setTotal(totalTransaction)
    setTransactions(data)
  }

  async function reloadData(toReloadData: boolean) {
    if (!toReloadData) return
    await fetchData()
  }

  useEffect(() => {
    fetchData()
  }, [])
  return (
    <>
      <main className="w-[100vw] h-[100vh] bg-black bg-opacity-95 space-y-4">
        <section className="w-[30%] mx-auto py-5 rounded-lg border border-white p-2">
          <h1 className="text-white text-xl font-bold">Total disponivel : <span className={`${total >= 0 ? 'text-green-500' : 'text-red-500'} `}>{total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</span></h1>
        </section>
        <section className="w-full flex flex-wrap px-10">
          {
            transactions.map((transaction) =>
              <Card key={transaction.id} className="w-[25%] bg-slate-900 bg-opacity-70 border-0 text-white">
                <div className="w-full p-0 flex justify-end">
                  <DeleteTransactionModal reloadData={reloadData} transactionId={transaction.id} />
                </div>
                <CardHeader className="w-full pt-0 flex flex-row justify-between items-center">
                  <CardTitle>
                    {transaction.type === 1 ? "Crédito" : "Débido"}
                  </CardTitle>
                  {
                    transaction.type === 1 &&
                    (
                      <InstallmentModal reloadData={reloadData} transactionId={transaction.id} />
                    )
                  }
                </CardHeader>
                <CardContent className="space-y-2 pb-0">
                  <div className="space-y-2">
                    <div className="flex justify-between">
                      <p className="text-lg font-semibold">{transaction.author}</p>
                      <p className={`text-lg font-semibold ${transaction.type === 1 ? 'text-red-600' : 'text-green-500'}`}>{transaction.amount.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
                      <p className="text-lg font-semibold">{timestampToUser(transaction.date)}</p>
                    </div>
                    <div>
                      <p className="text-lg font-semibold">Restante a pagar: R$ {transaction.amount - transaction.installments.reduce((acc, cur) => acc + cur.amount, 0)}</p>
                    </div>
                  </div>
                  <InstallmentComponent reloadData={reloadData} installments={transaction.installments} />
                </CardContent>
              </Card>
            )
          }
        </section>
      </main>
      <Toaster />
    </>
  )
}
