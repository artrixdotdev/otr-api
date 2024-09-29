using System.ComponentModel;

namespace API.Utilities;

/// <summary>
/// String values that represent valid claims for authorized users
/// </summary>
public static class OtrClaims
{
    /// <summary>
    /// Claim granted to all users
    /// </summary>
    public const string User = "user";

    /// <summary>
    /// Claim granted to all clients
    /// </summary>
    public const string Client = "client";

    /// <summary>
    /// Claim granted to internal privileged clients
    /// </summary>
    /// <example>o!TR processor</example>
    public const string System = "system";

    /// <summary>
    /// Claim granted to privileged users
    /// </summary>
    public const string Admin = "admin";

    /// <summary>
    /// Claim granted to users with permission to verify submission data
    /// </summary>
    public const string Verifier = "verifier";

    // TODO: Convert this to work in the inverse
    // Instead of granting all users "submit", we can grant restricted users "restricted" which would
    // flag the submission flow to check the user for potential submission restriction
    /// <summary>
    /// Claim granted to users with permission to submit tournament data
    /// </summary>
    public const string Submitter = "submit";

    /// <summary>
    /// Claim granted to users and clients to allow access during times of restricted use
    /// </summary>
    public const string Whitelist = "whitelist";

    /// <summary>
    /// Claim granted to users or clients to denote overrides to the default rate limit
    /// </summary>
    public const string RateLimitOverrides = "ratelimitoverrides";

    /// <summary>
    /// Gets the description for the given claim
    /// </summary>
    public static string GetDescription(string claim) =>
        claim switch
        {
            User => "Claim granted to all users",
            Client => "Claim granted to all users",
            System => "Claim granted to internal privileged clients",
            Admin => "Claim granted to privileged users",
            Verifier => "Claim granted to users with permission to verify submission data",
            Submitter => "Claim granted to users with permission to submit tournament data",
            Whitelist => "Claim granted to users and clients to allow access during times of restricted use",
            _ => "Undocumented"
        };

    /// <summary>
    /// Denotes the given claim is assignable to a user
    /// </summary>
    public static bool IsUserAssignableClaim(string claim)
    {
        return claim switch
        {
            // 'User' not included because we only encode that claim to the JWT
            Admin => true,
            Verifier => true,
            Submitter => true,
            Whitelist => true,
            RateLimitOverrides => true,
            _ => false
        };
    }

    /// <summary>
    /// Denotes the given claim is assignable to a client
    /// </summary>
    public static bool IsClientAssignableClaim(string claim)
    {
        return claim switch
        {
            // 'Client' not included because we only encode that claim to the JWT
            System => true,
            Whitelist => true,
            RateLimitOverrides => true,
            _ => false
        };
    }

    /// <summary>
    /// Denotes the given claim is valid
    /// </summary>
    public static bool IsValidClaim(string claim)
    {
        return claim switch
        {
            User => true,
            Client => true,
            System => true,
            Admin => true,
            Verifier => true,
            Submitter => true,
            Whitelist => true,
            RateLimitOverrides => true,
            _ => false
        };
    }
}
