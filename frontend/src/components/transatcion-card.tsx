import { TTransaction, timestampToUser } from "@/lib/utils"
import {
    Card,
    CardContent,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"
import { DeleteTransactionModal, InstallmentModal } from "./modal"
import InstallmentComponent from "./installment-component"

export function TransactionCard({ transaction, reloadData }: { transaction: TTransaction, reloadData: (toReloadData: boolean) => void }) {
    return (
        <div className="w-1/4" key={transaction.id} >
            <Card className="w-full bg-slate-900 bg-opacity-70 border-0 text-white">
                <div className="w-full p-0 flex justify-end">
                    <DeleteTransactionModal reloadData={reloadData} transactionId={transaction.id} />
                </div>
                <CardHeader className="w-full h-10 flex flex-row justify-between items-center">
                    <CardTitle className={transaction.type === 1 ? 'text-red-600' : 'text-green-500'}>
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
                            <p className={`text-lg font-semibold `}>{transaction.amount.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
                            <p className="text-lg font-semibold">{timestampToUser(transaction.date)}</p>
                        </div>
                        <p className="text-lg font-semibold">{transaction.description}</p>
                        <div className="h-7">
                            {transaction.type === 1 && <p className="text-lg font-semibold">Restante a pagar: R$ {transaction.amount - transaction.installments.reduce((acc, cur) => acc + cur.amount, 0)}</p>}
                        </div>
                    </div>
                    <InstallmentComponent installments={transaction.installments} reloadData={reloadData} />
                </CardContent>
            </Card>
        </div>
    )
}
