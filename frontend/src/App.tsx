
import { useEffect, useState } from "react"
import { TTransaction, axiosInstance } from "./lib/utils"
import { Toaster } from "./components/ui/toaster"
import { TransactionModal } from "./components/modal"
import { TransactionCard } from "./components/transatcion-card"

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
        <section className="w-[30%] mx-auto rounded-lg border border-white p-2">
          <h1 className="text-white text-xl font-bold">Total disponivel : <span className={`${total >= 0 ? 'text-green-500' : 'text-red-500'} `}>{total.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</span></h1>
          <TransactionModal reloadData={reloadData} />
        </section>
        <section className="w-full flex flex-wrap gap-3 px-10">
          {
            transactions.map((transaction) =>
              <TransactionCard transaction={transaction} reloadData={reloadData} />
            )
          }
        </section>
      </main >
      <Toaster />
    </>
  )
}
