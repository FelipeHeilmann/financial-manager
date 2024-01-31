import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { useEffect, useState } from "react"
import { TTransaction, axiosInstance, timestampToUser } from "./lib/utils"
import { InstallmentModal } from "./components/modal"
import {
  Accordion,
  AccordionContent,
  AccordionItem,
  AccordionTrigger,
} from "@/components/ui/accordion"
import { ScrollArea } from "@/components/ui/scroll-area"

export default function App() {
  const [total, setTotal] = useState(0)
  const [transactions, setTransactions] = useState<TTransaction[]>([])

  const fetchData = async () => {
    const { data } = await axiosInstance.get("/api/transactions") as { data: TTransaction[] }

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
          transactions.map((transaction) =>
            <Card key={transaction.id} className="w-[25%] bg-slate-900 bg-opacity-70 border-0 text-white">
              <CardHeader className="w-full flex flex-row justify-between items-center">
                <CardTitle>
                  {transaction.type === 1 ? "Crédito" : "Débido"}
                </CardTitle>
                {
                  transaction.type === 1 &&
                  (
                    <InstallmentModal transactionId={transaction.id} />
                  )
                }
              </CardHeader>
              <CardContent className="space-y-3">
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
                <Accordion type="single" collapsible>
                  <AccordionItem value="item-1">
                    <AccordionTrigger className="justify-end"></AccordionTrigger>
                    <AccordionContent>
                      <ScrollArea className="h-72 w-full">
                        <div className="space-y-2">
                          {
                            transaction.installments.map((installment, index) =>
                              <div key={installment.id} className="w-full bg-slate-800 p-2 rounded-lg space-y-2">
                                <h2 className="font-bold text-xl">Parcela {index + 1}</h2>
                                <p className="text-lg font-semibold">{installment.amount.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
                                <p className="text-lg font-semibold">{timestampToUser(installment.date)}</p>
                              </div>
                            )
                          }
                        </div>
                      </ScrollArea>
                    </AccordionContent>
                  </AccordionItem>
                </Accordion>
              </CardContent>
            </Card>
          )
        }
      </section>
    </main >
  )
}
