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
        // TODO: подписаться на coin.mention (Redis Stream) вместо мок-списка.
        // Каждое новое упоминание -> добавить mint в множество отслеживаемых.
        var watchedMints = LoadMockWatchedMints();

        foreach (var mint in watchedMints)
        {
            // TODO: getParsedAccountInfo(mint) через выбранный RPC-провайдер (Helius Free).
            var (mintAuthority, freezeAuthority) = await FetchMintAuthoritiesAsync(mint);

            // TODO: эти три поля — из отдельного вендора (RugCheck/Solana Tracker),
            // не из RPC. Провайдер не выбран, пока заглушка.
            var (lpLocked, bundledPct, sniperPct) = await FetchRiskMetricsAsync(mint);

            await PublishOnchainSnapshotAsync(mint, mintAuthority, freezeAuthority,
                lpLocked, bundledPct, sniperPct);
        }

        Console.WriteLine("ingestor-onchain: готово (мок-прогон).");
    }

    private static IEnumerable<string> LoadMockWatchedMints()
    {
        yield break; // заглушка — заменить чтением из coin.mention
    }

    private static Task<(string? mintAuthority, string? freezeAuthority)> FetchMintAuthoritiesAsync(string mint)
    {
        // TODO: getParsedAccountInfo(mint, {encoding: "jsonParsed"}) через RPC-провайдера.
        // SPL Token Mint layout содержит mintAuthority/freezeAuthority напрямую —
        // не нужно вручную парсить бинарные данные.
        throw new NotImplementedException("RPC-клиент ещё не подключён");
    }

    private static Task<(bool? lpLocked, float? bundledPct, float? sniperPct)> FetchRiskMetricsAsync(string mint)
    {
        // TODO: провайдер риск-метрик не выбран (см. providers.md).
        throw new NotImplementedException("вендор риск-метрик ещё не выбран");
    }

    private static Task PublishOnchainSnapshotAsync(
        string mint, string? mintAuthority, string? freezeAuthority,
        bool? lpLocked, float? bundledPct, float? sniperPct)
        => throw new NotImplementedException("публикация onchain.snapshot");
}
