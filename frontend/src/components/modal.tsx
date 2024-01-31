import {
    Dialog,
    DialogClose,
    DialogContent,
    DialogFooter,
    DialogHeader,
    DialogTitle,
    DialogTrigger,
} from "@/components/ui/dialog"
import { TCreateInstallment, axiosInstance, installmentZodSchema } from "@/lib/utils"
import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { Input } from "./ui/input"
import { Button } from "./ui/button"
import { Trash } from "lucide-react"


export function InstallmentModal({ transactionId, reloadData }: { transactionId: string, reloadData: (toReloadData: boolean) => void }) {

    const { register, handleSubmit } = useForm<TCreateInstallment>({
        resolver: zodResolver(installmentZodSchema)
    })

    async function handleCreateInstallmentSubmit(data: TCreateInstallment) {
        const input = {
            amount: data.amount,
            date: new Date(data.date).toISOString(),
            transactionId
        }

        await axiosInstance.post('/api/installments', input)
        await reloadData(true)
        document.getElementById('closeDialog')?.click()
    }
    return (
        <Dialog>
            <DialogTrigger asChild>
                <button className="text-md border p-1 rounded-md transition-all hover:bg-slate-50 hover:text-black">Criar parcela</button>
            </DialogTrigger>
            <DialogContent className="bg-slate-900 border-0">
                <DialogHeader>
                    <DialogTitle className="text-xl text-white">
                        Nova parcela
                    </DialogTitle>
                </DialogHeader>
                <form onSubmit={handleSubmit(handleCreateInstallmentSubmit)} className="space-y-3">
                    <div className="grid grid-cols-4 items-center gap-2">
                        <label className="text-white col-span-1" htmlFor="">Valor</label>
                        <Input className="h-8 p-1 col-span-3" type="text" {...register("amount")} />
                    </div>
                    <div className="grid grid-cols-4 items-center gap-2">
                        <label className="text-white" htmlFor="">Data</label>
                        <Input className="h-8 p-1 col-span-3" type="date" {...register("date")} />
                    </div>
                    <DialogFooter>
                        <DialogClose asChild>
                            <button className="text-white font-normal border p-1 rounded-md transition-all hover:bg-slate-50 hover:text-black">Cancelar</button>
                        </DialogClose>
                        <button type="submit" className="text-white font-normal border p-1 rounded-md transition-all hover:bg-slate-50 hover:text-black">Salvar</button>
                    </DialogFooter>
                </form>
            </DialogContent>
        </Dialog>
    )
}

export function DeleteInstallmentModal({ installmentId, reloadData }: { installmentId: string, reloadData: (toReloadData: boolean) => void }) {
    async function handleDeleteInstallment() {
        await axiosInstance.delete(`/api/installments/${installmentId}`)
        await reloadData(true)
        document.getElementById('closeDialog')?.click()
    }
    return (
        <Dialog>
            <DialogTrigger asChild>
                <Button className="h-8 px-2">
                    <Trash color="#dc2626" />
                </Button>
            </DialogTrigger>
            <DialogContent className="bg-slate-900 border-0">
                <DialogHeader>
                    <DialogTitle className="text-xl text-white">
                        Apagar parcela
                    </DialogTitle>
                </DialogHeader>
                <h2 className="text-white text-xl">Deseja mesmo apagar essa parcela?</h2>
                <DialogFooter>
                    <DialogClose asChild>
                        <button className="text-white font-normal border p-1 rounded-md transition-all hover:bg-slate-50 hover:text-black">Cancelar</button>
                    </DialogClose>
                    <button type="submit" onClick={handleDeleteInstallment} className="text-white font-normal border p-1 rounded-md transition-all hover:bg-slate-50 hover:text-black">Apagar</button>
                </DialogFooter>
            </DialogContent>
        </Dialog >
    )
}