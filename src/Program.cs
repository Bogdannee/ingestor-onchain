// ingestor-onchain — ловит новые токены и их on-chain состояние.
// Источник монет НЕЗАВИСИМ от твитов: подписка на Solana RPC (logsSubscribe),
// декодирование логов программы pump.fun.
// Эмитит: mint.new (появился новый токен), onchain.snapshot (права контракта,
// лок ликвидности и т.п.).
//
// Сейчас (T11, неделя 3): по mint отдаёт мок/free-tier снапшот, публикует событие.
// Провайдер (Helius / Alchemy, бесплатный тариф) — форма ответа подтверждается в T5/T13.

namespace IngestorOnchain;

public static class Program
{
    public static async Task Main(string[] args)
    {
        // TODO(T11): вместо реальной подписки logsSubscribe — мок-источник
        // новых mint'ов (файл/захардкоженный список для локального прогона).

        foreach (var mint in LoadMockMints())
        {
            var snapshot = await FetchOnchainSnapshotAsync(mint);
            await PublishMintNewAsync(mint);
            await PublishOnchainSnapshotAsync(mint, snapshot);
        }

        Console.WriteLine("ingestor-onchain: готово (мок-прогон).");
    }

    private static IEnumerable<string> LoadMockMints()
    {
        yield break; // заглушка
    }

    private static Task<object> FetchOnchainSnapshotAsync(string mint)
    {
        // TODO: mint/freeze authority, lp_locked, bundled_pct, sniper_pct
        throw new NotImplementedException("T11: провайдер ещё не подключён");
    }

    private static Task PublishMintNewAsync(string mint)
        => throw new NotImplementedException("T11: публикация mint.new");

    private static Task PublishOnchainSnapshotAsync(string mint, object snapshot)
        => throw new NotImplementedException("T11: публикация onchain.snapshot");
}
