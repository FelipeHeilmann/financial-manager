import { TInstallment, timestampToUser } from "@/lib/utils"
import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
} from "@/components/ui/accordion"
import { ScrollArea } from "@/components/ui/scroll-area"
import { DeleteInstallmentModal } from "./modal"

export default function InstallmentComponent({ installments, reloadData }: { installments: TInstallment[], reloadData: (toReloadData: boolean) => void }) {
    return (
        <>
            <Accordion type="single" collapsible>
                <AccordionItem value="item-1">
                    <AccordionTrigger className="justify-end"></AccordionTrigger>
                    <AccordionContent className="">
                        <ScrollArea className="w-full">
                            <div className="space-y-2">
                                {
                                    installments && installments.map((installment, index) =>
                                        <div key={installment.id} className="w-full bg-slate-800 p-2 rounded-lg space-y-2 px-1">
                                            <div className="flex justify-between">
                                                <h2 className="font-bold text-xl">Parcela {index + 1}</h2>
                                                <DeleteInstallmentModal reloadData={reloadData} installmentId={installment.id} />
                                            </div>
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
        </>
    )
}   