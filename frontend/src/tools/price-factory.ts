export default function DisplayPrice(amount?: number | undefined, currency?: string | undefined) {
    return (amount! / 100).toFixed(2) + ' ' + currency
}