--
-- Struktura tabulky `gcm_users`
--

CREATE TABLE IF NOT EXISTS `gcm_users` (
`id` int(11) NOT NULL,
  `gcm_regid` varchar(255) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB  DEFAULT CHARSET=utf8 AUTO_INCREMENT=30 ;

--
-- Klíče pro exportované tabulky
--

--
-- Klíče pro tabulku `gcm_users`
--
ALTER TABLE `gcm_users`
 ADD PRIMARY KEY (`id`), ADD UNIQUE KEY `uc_regid` (`gcm_regid`);

--
-- AUTO_INCREMENT pro tabulky
--

--
-- AUTO_INCREMENT pro tabulku `gcm_users`
--
ALTER TABLE `gcm_users`
MODIFY `id` int(11) NOT NULL AUTO_INCREMENT,AUTO_INCREMENT=30;
